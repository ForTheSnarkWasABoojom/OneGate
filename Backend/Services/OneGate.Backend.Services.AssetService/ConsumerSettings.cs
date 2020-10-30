using MassTransit.Definition;

namespace OneGate.Backend.Services.AssetService
{
    public class ConsumerSettings : ConsumerDefinition<Consumer>
    {
        public ConsumerSettings()
        {
            EndpointName = "asset-service";
        }
    }
}