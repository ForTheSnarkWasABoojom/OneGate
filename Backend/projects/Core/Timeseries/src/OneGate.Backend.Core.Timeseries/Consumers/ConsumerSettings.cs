using MassTransit.Definition;

namespace OneGate.Backend.Core.Timeseries.Consumers
{
    public class ConsumerSettings : ConsumerDefinition<Consumer>
    {
        public ConsumerSettings()
        {
            EndpointName = "timeseries-service";
        }
    }
}