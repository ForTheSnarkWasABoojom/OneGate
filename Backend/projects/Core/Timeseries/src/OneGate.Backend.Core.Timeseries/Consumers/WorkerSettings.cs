using MassTransit.Definition;

namespace OneGate.Backend.Core.Timeseries.Consumers
{
    public class WorkerSettings : ConsumerDefinition<Worker>
    {
        public WorkerSettings()
        {
            EndpointName = "timeseries-worker";
        }
    }
}