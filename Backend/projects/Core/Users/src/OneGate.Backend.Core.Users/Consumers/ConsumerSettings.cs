using MassTransit.Definition;

namespace OneGate.Backend.Core.Users.Consumers
{
    public class ConsumerSettings : ConsumerDefinition<Consumer>
    {
        public ConsumerSettings()
        {
            EndpointName = "user-service";
        }
    }
}