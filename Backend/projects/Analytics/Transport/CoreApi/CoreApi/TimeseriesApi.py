import requests
import pandas as pd
from datetime import datetime
from typing import Optional

from pandas import DataFrame


class TimeseriesApi:
    base_url = 'http://localhost:8083/api/v1'

    def __init__(self, base_url: Optional[str] = 'http://localhost:8083/api/v1'):
        self.base_url = base_url
        TimeseriesApi.base_url = base_url

    @staticmethod
    def get_ohlc(asset_id: int, interval: str,
                 start_time: Optional[datetime] = datetime(year=1900, month=1, day=1),
                 end_time: Optional[datetime] = datetime.now(),
                 count: Optional[int] = 99999999, shift: Optional[int] = 0) -> DataFrame:
        res = requests.get(
            url=f'{TimeseriesApi.base_url}/ohlc?asset_id={asset_id}&interval={interval}&start_timestamp={start_time.isoformat()}'
                f'&end_timestamp={end_time.isoformat()}&shift={shift}&count={count}')
        df = pd.DataFrame(res.json())
        return df
