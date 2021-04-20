from abc import ABC, abstractmethod
import asyncio


class TransportPublisherABC(ABC):

    def __init__(self, input_queue_name, input_exchange_name, output_queue_name, output_exchange_name):
        self.input_queue_name = input_queue_name
        self.input_exchange_name = input_exchange_name
        self.output_queue_name = output_queue_name
        self.output_exchange_name = output_exchange_name
        self.loop = asyncio.get_event_loop()

    @abstractmethod
    async def connect(self, callback_func):
        pass

    @abstractmethod
    async def get_response_json(self, complex_json):
        pass

    @abstractmethod
    async def send_message(self, job_id: int, buy_probability: float, sell_probability, hold_probability):
        pass
