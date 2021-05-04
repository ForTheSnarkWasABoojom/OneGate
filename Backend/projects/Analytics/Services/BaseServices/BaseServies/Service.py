from abc import abstractmethod, ABC

from TransportPublisher.TransportPublisher import TransportPublisher
from CoreApi.OhlcCache import OhlcCache

from .OhlcEnum import OhlcEnum


class ServiceABC(ABC):
    def __init__(self, input_queue_name, input_exchange_name, output_queue_name, output_exchange_name):
        self.publisher = TransportPublisher(input_queue_name=input_queue_name,
                                            input_exchange_name=input_exchange_name,
                                            output_queue_name=output_queue_name,
                                            output_exchange_name=output_exchange_name)
        self.cache = OhlcCache()
        self.ohlc = OhlcEnum
        self.intervals = ['m1', 'm5', 'm15', 'm30', 'H1', 'H4', 'D', 'M1']
        self.cols = ['low', 'high', 'open', 'close', 'timestamp']

    @abstractmethod
    async def callback(self, body):
        pass

    @abstractmethod
    async def _setup(self):
        pass

    @abstractmethod
    async def run(self):
        pass
