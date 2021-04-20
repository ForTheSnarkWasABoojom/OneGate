import asyncio
import requests
import uuid

class AnalyticsApi:
    def __init__(self):
        self.guid = str(uuid.uuid4())
