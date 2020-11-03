using System.Threading.Tasks;
using MassTransit;
using OneGate.Backend.Contracts.Timeseries;
using OneGate.Backend.Rpc;
using OneGate.Backend.Rpc.Services;

namespace OneGate.Backend.Services.TimeseriesService
{
    public class Consumer :
        IConsumer<OnOhlcTimeseriesUpdated>,
        IConsumer<CreateOhlcTimeseries>,
        IConsumer<GetOhlcTimeseries>,
        IConsumer<DeleteOhlcTimeseries>,
        IConsumer<CreateValueTimeseries>,
        IConsumer<GetValueTimeseries>,
        IConsumer<DeleteValueTimeseries>
    {
        private readonly ITimeseriesService _service;

        public Consumer(ITimeseriesService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<OnOhlcTimeseriesUpdated> context)
        {
            await _service.OnOhlcTimeseriesUpdated(context.Message);
        }

        public async Task Consume(ConsumeContext<CreateOhlcTimeseries> context)
        {
            await context.MarshallWith(_service.CreateOhlcTimeseriesRange);
        }

        public async Task Consume(ConsumeContext<GetOhlcTimeseries> context)
        {
            await context.MarshallWith(_service.GetOhlcTimeseriessRange);
        }

        public async Task Consume(ConsumeContext<DeleteOhlcTimeseries> context)
        {
            await context.MarshallWith(_service.DeleteOhlcTimeseriesRange);
        }

        public async Task Consume(ConsumeContext<CreateValueTimeseries> context)
        {
            await context.MarshallWith(_service.CreateValueTimeseriesRange);
        }

        public async Task Consume(ConsumeContext<GetValueTimeseries> context)
        {
            await context.MarshallWith(_service.GetValueTimeseriessRange);
        }

        public async Task Consume(ConsumeContext<DeleteValueTimeseries> context)
        {
            await context.MarshallWith(_service.DeleteValueTimeseriesRange);
        }
    }
}