using System;
using EasyNetQ;

namespace OneGate.Backend.Rpc
{
    public static class BusFactory
    {
        public static IBus GetInstance()
        {
            return RabbitHutch.CreateBus("host=rabbitmq;timeout=25;" +
                                         $"username={Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_USER")};" +
                                         $"password={Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_PASS")}");
        }
    }
}