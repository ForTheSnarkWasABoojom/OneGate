from datetime import datetime, timedelta

import pytest
from ContractModels.CreateAnalyticsReport import CreateAnalyticsReport
from pydantic.tools import parse_obj_as

from ..CoreApi.OhlcCache import OhlcCache


@pytest.mark.parametrize("create_report, window_size",
                         [
                             ({'timestamp': datetime.now(),
                               'job': {'id': 1, 'provider': 'ss', 'metric': 'ss', 'interval': 'D', 'asset_id': 666,
                                       'options': 'opt'}}, 55),
                             (({'timestamp': datetime.now() - timedelta(days=365 * 20),
                                'job': {'id': 1, 'provider': 'ss', 'metric': 'ss', 'interval': 'D', 'asset_id': 666,
                                        'options': 'opt'}}, 100))
                         ])
def test_get_missing_ohlc_df_lenght_1(create_report: CreateAnalyticsReport,
                                      window_size: int):
    """
    Not empty df case
    """
    l1 = {'asset_id': 0, 'interval': 'D', 'high': 28.125, 'low': 25.42188, 'open': 26.21875, 'volume': 0,
          'timestamp': datetime(year=2000, month=4, day=29)}
    t = OhlcCache()
    report = parse_obj_as(CreateAnalyticsReport,create_report)
    update_df = t.get_ohlc(report, window_size)
    lenght = len(update_df)
    assert lenght == window_size


@pytest.mark.parametrize("create_report, window_size",
                         [
                             ({'timestamp': datetime.now(),
                               'job': {'id': 1, 'provider': 'ss', 'metric': 'ss', 'interval': 'D', 'asset_id': 666,
                                       'options': 'opt'}}, 55),
                             (({'timestamp': datetime.now() - timedelta(days=365 * 21),
                                'job': {'id': 1, 'provider': 'ss', 'metric': 'ss', 'interval': 'D', 'asset_id': 666,
                                        'options': 'opt'}}, 100))
                         ])
def get_missing_ohlc_common(create_report: CreateAnalyticsReport,
                            window_size: int):
    t = OhlcCache()
    report = parse_obj_as(CreateAnalyticsReport, create_report)
    update_df = t.get_ohlc(report, window_size)