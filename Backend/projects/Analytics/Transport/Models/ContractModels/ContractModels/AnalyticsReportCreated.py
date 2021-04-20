from datetime import datetime
from pydantic import BaseModel


class AnalyticsReportCreated(BaseModel):
    job_id: str  # routing key
    timestamp: datetime

    buy_probability: float = 0
    sell_probability: float = 0
    hold_probability: float = 0
