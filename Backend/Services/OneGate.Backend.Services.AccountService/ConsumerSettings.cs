using MassTransit.Definition;

namespace OneGate.Backend.Services.AccountService
{
    public class ConsumerSettings : ConsumerDefinition<Consumer>
    {
        public ConsumerSettings()
        {
            EndpointName = "account-service";
        }
    }
}