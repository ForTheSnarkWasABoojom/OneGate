using MassTransit.Definition;

namespace OneGate.Backend.Core.Records.Consumers
{
    public class ConsumerSettings : ConsumerDefinition<Consumer>
    {
        public ConsumerSettings()
        {
            EndpointName = "asset-service";
        }
    }
}