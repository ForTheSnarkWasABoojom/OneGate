from enum import Enum


class OhlcEnum(str, Enum):
    open = "open"
    close = "close"
    high = "high"
    low = "low"
    timestamp = "timestamp"
