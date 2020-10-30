using MassTransit.Definition;

namespace OneGate.Backend.Gateway.Consumers
{
    public class AccountConsumerSettings : ConsumerDefinition<AccountConsumer>
    {
        public AccountConsumerSettings()
        {
            EndpointName = "gateway-account-events";
        }
    }
}