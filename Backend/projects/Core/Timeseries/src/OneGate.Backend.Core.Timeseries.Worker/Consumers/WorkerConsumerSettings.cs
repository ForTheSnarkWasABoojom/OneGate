using MassTransit.Definition;

namespace OneGate.Backend.Core.Timeseries.Worker.Consumers
{
    public class WorkerConsumerSettings :  ConsumerDefinition<WorkerConsumer>
    {
        public WorkerConsumerSettings()
        {
            EndpointName = "timeseries-worker";
        }
    }
}