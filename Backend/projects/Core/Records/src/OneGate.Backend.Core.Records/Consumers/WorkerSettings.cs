using MassTransit.Definition;

namespace OneGate.Backend.Core.Records.Consumers
{
    public class WorkerSettings : ConsumerDefinition<Worker>
    {
        public WorkerSettings()
        {
            EndpointName = "records-worker";
        }
    }
}