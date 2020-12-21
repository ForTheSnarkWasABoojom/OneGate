using MassTransit.Definition;

namespace OneGate.Backend.Core.Timeseries.Node
{
    public class ConsumerSettings : ConsumerDefinition<Consumer>
    {
        public ConsumerSettings()
        {
            EndpointName = "timeseries-service";
        }
    }
}