from datetime import datetime
from typing import List, Optional
import os

import asyncio
from sklearn.cluster import KMeans
import pandas as pd
import numpy

from BaseServies.ForecastService import ForecastService
from ContractModels.CreateAnalyticsReport import CreateAnalyticsReport


class Kmeans(ForecastService):

    def __init__(self, input_queue_name: str, input_exchange_name: str, output_queue_name: str,
                 output_exchange_name: str, window_size: Optional[float] = 200,
                 saturation_point: Optional[float] = 0.05):
        super().__init__(input_queue_name, input_exchange_name, output_queue_name, output_exchange_name)
        self.window_size = window_size
        self.saturation_point = saturation_point

    async def callback(self, body):
        buy_probability = 0.
        sell_probability = 0.
        hold_probability = 0.

        contract: CreateAnalyticsReport = await self.publisher.get_response_json(body)

        train_df = self.cache.get_ohlc(contract.job.id, contract.job.interval, contract.timestamp, self.window_size)
        lows, highs = await self.get_lines(train_df)
        curr_open = train_df.iloc[-1][self.ohlc.open.name]
        curr_close = train_df.iloc[-1][self.ohlc.close.name]

        for low in lows:
            if curr_open >= low >= curr_close:
                buy_probability = 100.
        for high in highs:
            if curr_open <= high <= curr_close:
                sell_probability = 100.

        if buy_probability == 0. and sell_probability == 0.:
            hold_probability = 100.
        await self.publisher.send_message(timestamp=contract.timestamp,
                                          job_id=contract.job.id,
                                          buy_probability=buy_probability,
                                          sell_probability=sell_probability,
                                          hold_probability=hold_probability,
                                          )

    async def get_optimum_clusters(self, df: pd.DataFrame):
        '''
        :param df: dataframe
        :param saturation_point: The amount of difference we are willing to detect
        :return: clusters with optimum K centers

        This method uses elbow method to find the optimum number of K clusters
        We initialize different K-means with 1..10 centers and compare the inertias
        If the difference is no more than saturation_point, we choose that as K and move on
        '''

        wcss = []
        k_models = []

        size = min(11, len(df.index))
        for i in range(1, size):
            kmeans = KMeans(n_clusters=i, init='k-means++', max_iter=100, n_init=10, random_state=0)
            kmeans.fit(df)
            wcss.append(kmeans.inertia_)
            k_models.append(kmeans)

        # Compare differences in inertias until it's no more than saturation_point
        optimum_k = len(wcss) - 1
        for i in range(0, len(wcss) - 1):
            diff = abs(wcss[i + 1] - wcss[i])
            if diff < self.saturation_point:
                optimum_k = i
                break

        optimum_clusters = k_models[optimum_k]

        return optimum_clusters

    async def get_lines(self, data: pd.DataFrame):
        lows = pd.DataFrame(data=data, index=data.index, columns=[self.ohlc.low.name])
        highs = pd.DataFrame(data=data, index=data.index, columns=[self.ohlc.high.name])

        low_clusters = await self.get_optimum_clusters(lows)
        low_centers = low_clusters.cluster_centers_
        low_centers = numpy.sort(low_centers, axis=0)

        high_clusters = await self.get_optimum_clusters(highs)
        high_centers = high_clusters.cluster_centers_
        high_centers = numpy.sort(high_centers, axis=0)

        return low_centers, high_centers

    async def _setup(self):
        pass

    async def run(self):
        await self._setup()
        await self.publisher.connect(self.callback)

    async def backtest(self, asset_id: int, interval: str, date_from: datetime, date_to: datetime) -> List[float]:
        pass
