using MassTransit.Definition;

namespace OneGate.Backend.Core.AccountService
{
    public class ConsumerSettings : ConsumerDefinition<Consumer>
    {
        public ConsumerSettings()
        {
            EndpointName = "account-service";
        }
    }
}