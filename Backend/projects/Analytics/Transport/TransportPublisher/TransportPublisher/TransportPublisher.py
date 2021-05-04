import json
import os
from datetime import datetime
from typing import Optional

from ContractModels.AnalyticsReportCreated import AnalyticsReportCreated
from aio_pika import connect, Message
import asyncio
from pydantic import parse_obj_as

from .TransportPublisherABC import TransportPublisherABC
from ContractModels.CreateAnalyticsReport import CreateAnalyticsReport


class TransportPublisher(TransportPublisherABC):
    async def output_connect(self) -> None:
        is_connected_output = True
        while is_connected_output:
            try:
                # Perform connection
                output_connection = await connect(
                    url=os.environ.get('OneGate__RabbitMq__URL'),
                    host=os.environ.get('OneGate__RabbitMq__Host'),
                    login=os.environ.get('OneGate__RabbitMQ__User'),
                    password=os.environ.get('OneGate__RabbitMQ__Password'),
                    loop=self.loop
                )
                # Creating a channel
                self.send_channel = await output_connection.channel()
                self.send_queue = await self.send_channel.declare_queue(
                    self.output_queue_name,
                    durable=True
                )
                self.output_exchange = await self.send_channel.declare_exchange(self.output_exchange_name)
                await self.send_queue.bind(routing_key=self.output_exchange_name, exchange=self.output_exchange)
                is_connected_output = False
            except Exception as e:
                await asyncio.sleep(1)

    async def input_connect(self, callback_func) -> None:
        is_connected_input = True
        while is_connected_input:
            try:
                self.input_connection = await connect(
                    url=os.environ.get('OneGate__RabbitMq__URL'),
                    host=os.environ.get('OneGate__RabbitMq__Host'),
                    login=os.environ.get('OneGate__RabbitMQ__User'),
                    password=os.environ.get('OneGate__RabbitMQ__Password'),
                    loop=self.loop)

                self.receive_channel = await self.input_connection.channel()

                # await self.receive_channel.set_qos(prefetch_count=1)
                self.receive_queue = await self.receive_channel.declare_queue(
                    self.input_queue_name,
                    durable=True
                )
                self.input_exchange = await self.receive_channel.declare_exchange(self.input_exchange_name)
                await self.receive_queue.bind(routing_key=self.input_exchange_name, exchange=self.input_exchange)
                # Blocking connection
                await self.receive_queue.consume(callback_func)
                is_connected_input = False
            except Exception as e:
                await asyncio.sleep(1)

    async def connect(self, callback_func) -> None:

        await self.output_connect()
        await self.input_connect(callback_func=callback_func)

    async def get_response_json(self, complex_json) -> CreateAnalyticsReport:
        report = json.loads(complex_json.body)
        return parse_obj_as(CreateAnalyticsReport, report)

    async def send_message(self, timestamp: datetime, job_id: int, buy_probability: Optional[float] = 0,
                           sell_probability: Optional[float] = 0, hold_probability: Optional[float] = 0) -> None:
        response = AnalyticsReportCreated(buy_probability=buy_probability,
                                          sell_probability=sell_probability,
                                          hold_probability=hold_probability,
                                          job_id=job_id,
                                          timestamp=timestamp)
        message = Message(
            body=bytes(response.json(), encoding='utf-8'))

        await self.output_exchange.publish(
            message, routing_key=self.output_exchange_name,
        )

