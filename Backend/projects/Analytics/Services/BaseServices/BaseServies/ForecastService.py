from abc import abstractmethod
from typing import List
from datetime import datetime

from .Service import ServiceABC


class ForecastService(ServiceABC):

    @abstractmethod
    async def backtest(self, asset_id: int, interval: str, date_from: datetime, date_to: datetime) -> List[float]:
        pass
