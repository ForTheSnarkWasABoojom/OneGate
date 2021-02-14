using MassTransit.Definition;

namespace OneGate.Backend.Core.Users.Consumers
{
    public class RpcWorkerSettings : ConsumerDefinition<RpcWorker>
    {
        public RpcWorkerSettings()
        {
            EndpointName = "users-rpc-worker";
        }
    }
}