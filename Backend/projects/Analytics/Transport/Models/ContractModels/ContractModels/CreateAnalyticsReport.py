from datetime import datetime

from pydantic import BaseModel
from pydantic import parse_obj_as
from .AnalyticJobDto import AnalyticJobDto


class CreateAnalyticsReport(BaseModel):
    job: AnalyticJobDto
    timestamp: datetime
