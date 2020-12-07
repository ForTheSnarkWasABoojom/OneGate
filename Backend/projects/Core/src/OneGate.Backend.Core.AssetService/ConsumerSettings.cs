using MassTransit.Definition;

namespace OneGate.Backend.Core.AssetService
{
    public class ConsumerSettings : ConsumerDefinition<Consumer>
    {
        public ConsumerSettings()
        {
            EndpointName = "asset-service";
        }
    }
}