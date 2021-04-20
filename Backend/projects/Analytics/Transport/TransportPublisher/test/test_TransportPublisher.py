from datetime import datetime, timedelta

from ..TransportPublisher.TransportPublisher import TransportPublisher
import pytest


@pytest.mark.parametrize("complex_json",
                         [
                             {'timestamp': datetime.now(),
                              'job': {'id': 1, 'provider': 'a', 'metric': 'a', 'interval': 'D', 'asset_id': 2,
                                      'options': 'some options'}},
                             {'timestamp': datetime.now() - timedelta(days=100),
                              'job': {'id': 999, 'provider': 'ohlc exchange', 'metric': 'metrica', 'interval': 'm1',
                                      'asset_id': 898,
                                      'options': 'some options'}}
                         ])
def test_get_responce_json(complex_json):
    t = TransportPublisher('q1', 'ex', 'q2', 'ex2')
    res = t.get_response_json(complex_json)
