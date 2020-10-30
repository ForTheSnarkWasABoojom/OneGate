using MassTransit.Definition;

namespace OneGate.Backend.Gateway.Consumers
{
    public class TimeseriesConsumerSettings : ConsumerDefinition<TimeseriesConsumer>
    {
        public TimeseriesConsumerSettings()
        {
            EndpointName = "gateway-timeseries-events";
        }
    }
}