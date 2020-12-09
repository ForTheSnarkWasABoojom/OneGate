using MassTransit.Definition;

namespace OneGate.Backend.Core.Asset.Service
{
    public class ConsumerSettings : ConsumerDefinition<Consumer>
    {
        public ConsumerSettings()
        {
            EndpointName = "asset-service";
        }
    }
}