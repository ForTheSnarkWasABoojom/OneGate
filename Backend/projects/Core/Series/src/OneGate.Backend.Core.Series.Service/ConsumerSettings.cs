using MassTransit.Definition;

namespace OneGate.Backend.Core.Series.Service
{
    public class ConsumerSettings : ConsumerDefinition<Consumer>
    {
        public ConsumerSettings()
        {
            EndpointName = "timeseries-service";
        }
    }
}