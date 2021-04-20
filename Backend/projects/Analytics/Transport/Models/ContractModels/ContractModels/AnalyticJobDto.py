from pydantic import BaseModel
from pydantic import parse_obj_as


class AnalyticJobDto(BaseModel):
    id: int
    provider: str
    metric: str
    interval: str
    asset_id: int
    options: str


