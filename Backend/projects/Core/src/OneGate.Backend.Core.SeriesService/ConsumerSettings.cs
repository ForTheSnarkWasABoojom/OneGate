using MassTransit.Definition;

namespace OneGate.Backend.Core.SeriesService
{
    public class ConsumerSettings : ConsumerDefinition<Consumer>
    {
        public ConsumerSettings()
        {
            EndpointName = "timeseries-service";
        }
    }
}