using MassTransit.Definition;

namespace OneGate.Backend.Core.Assets.Consumers
{
    public class RpcWorkerSettings : ConsumerDefinition<RpcWorker>
    {
        public RpcWorkerSettings()
        {
            EndpointName = "assets-rpc-worker";
        }
    }
}