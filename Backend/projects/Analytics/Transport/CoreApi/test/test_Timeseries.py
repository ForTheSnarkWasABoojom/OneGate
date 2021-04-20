from datetime import datetime
import pytest

from ..CoreApi.TimeseriesApi import TimeseriesApi

"""
Requires AAPL daily data!
Tested on data from 2000-03-05 to 2000-05-24
"""


@pytest.mark.parametrize("asset_id, interval, start_time, end_time, count, shift",
                         [
                             (0, 'D', datetime(year=1900, month=1, day=1), datetime.now(), 10, 0),
                             (0, 'D', datetime(year=1900, month=1, day=1), datetime.now(), 999, 0),
                         ])
def test_get_ohlc_lenght(asset_id: int, interval: str,
                         start_time: datetime,
                         end_time: datetime,
                         count: int, shift: int):
    df = TimeseriesApi.get_ohlc(asset_id=asset_id, interval=interval)
    lenght = len(df)
    assert len(df) <= lenght


@pytest.mark.parametrize("asset_id, interval, start_time, end_time, count, shift",
                         [
                             (0, 'D', datetime(year=1900, month=1, day=1), datetime.now(), 10, 0),
                             (0, 'D', datetime(year=1900, month=1, day=1), datetime.now(), 999, 0),
                         ])
def test_get_ohlc_common(asset_id: int, interval: str,
                         start_time: datetime,
                         end_time: datetime,
                         count: int, shift: int):
    t = TimeseriesApi()
    TimeseriesApi.get_ohlc(asset_id=asset_id, interval=interval)


