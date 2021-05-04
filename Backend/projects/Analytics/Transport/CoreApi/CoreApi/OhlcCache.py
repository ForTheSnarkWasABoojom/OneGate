from datetime import timedelta, datetime

import pandas as pd
from pandas import DataFrame
from typing import Dict, List

from .TimeseriesApi import TimeseriesApi
from ContractModels.CreateAnalyticsReport import CreateAnalyticsReport


class OhlcCache:
    data: Dict[str, Dict[str, pd.DataFrame]]
    intervals: List[str] = ['m1', 'm5', 'm15', 'm30', 'H1', 'H4', 'D', 'M1']
    df_columns: List[str] = ['high', 'low', 'open', 'close', 'timestamp']

    def __init__(self):
        self.data = {}

    def get_ohlc(self, asset_id: int, interval: str, timestamp: datetime,
                 window_size: int) -> DataFrame:
        """

        :param asset_id: id of needed asset
        :param interval: trading interval
        :param timestamp: datetime of analytics request
        :param window_size: size of asked ohlc data
        :return: asked DataFrame
        """

        self._check_df_existance(asset_id, interval)

        df = self.data[asset_id][interval]

        if len(df) == 0:
            start_date = timestamp - timedelta(days=window_size + 100)
        else:
            last_line = df.iloc[-1]
            start_date = last_line['timestamp']
        update_df = TimeseriesApi.get_ohlc(asset_id=asset_id, interval=interval,
                                           start_time=start_date, end_time=timestamp)
        df = df.append(update_df, ignore_index=True)
        df = df[-window_size:]
        return df

    def _check_df_existance(self, asset_id: int, interval: str) -> None:
        """
        ckecks if needed dataframe in self.data and creates if not
        :param create_report: report on analytics to get needed information
        :return: None
        """
        # check if asset_id already exists
        df_for_id = self.data.get(asset_id)
        if df_for_id is None:
            self.data[asset_id] = {}
            for interval in OhlcCache.intervals:
                self.data[asset_id][interval] = pd.DataFrame(
                    columns=OhlcCache.df_columns)

        # check if dataframe already exists
        df = self.data.get(asset_id).get(interval)
        if df is None:
            self.data[asset_id][interval] = pd.DataFrame(
                columns=OhlcCache.df_columns)
