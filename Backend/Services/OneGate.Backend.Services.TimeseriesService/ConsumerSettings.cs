using MassTransit.Definition;

namespace OneGate.Backend.Services.TimeseriesService
{
    public class ConsumerSettings : ConsumerDefinition<Consumer>
    {
        public ConsumerSettings()
        {
            EndpointName = "timeseries-service";
        }
    }
}