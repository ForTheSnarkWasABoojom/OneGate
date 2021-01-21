using MassTransit.Definition;

namespace OneGate.Backend.Core.Records
{
    public class ConsumerSettings : ConsumerDefinition<Consumer>
    {
        public ConsumerSettings()
        {
            EndpointName = "asset-service";
        }
    }
}