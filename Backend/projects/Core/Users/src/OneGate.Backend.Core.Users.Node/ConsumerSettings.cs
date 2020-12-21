using MassTransit.Definition;

namespace OneGate.Backend.Core.Users.Node
{
    public class ConsumerSettings : ConsumerDefinition<Consumer>
    {
        public ConsumerSettings()
        {
            EndpointName = "user-service";
        }
    }
}