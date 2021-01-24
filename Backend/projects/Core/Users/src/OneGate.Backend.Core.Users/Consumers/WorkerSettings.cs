using MassTransit.Definition;

namespace OneGate.Backend.Core.Users.Consumers
{
    public class WorkerSettings : ConsumerDefinition<Worker>
    {
        public WorkerSettings()
        {
            EndpointName = "users-worker";
        }
    }
}