using MassTransit.Definition;

namespace OneGate.Backend.Core.Timeseries.Consumers
{
    public class RpcWorkerSettings : ConsumerDefinition<RpcWorker>
    {
        public RpcWorkerSettings()
        {
            EndpointName = "timeseries-rpc-worker";
        }
    }
}