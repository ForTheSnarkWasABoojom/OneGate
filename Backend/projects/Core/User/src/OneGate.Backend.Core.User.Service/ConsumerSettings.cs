using MassTransit.Definition;

namespace OneGate.Backend.Core.User.Service
{
    public class ConsumerSettings : ConsumerDefinition<Consumer>
    {
        public ConsumerSettings()
        {
            EndpointName = "user-service";
        }
    }
}