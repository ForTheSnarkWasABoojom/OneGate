using System.Threading.Tasks;
using MassTransit;
using OneGate.Backend.Contracts.OhlcTimeseries;
using OneGate.Backend.Contracts.ValueTimeseries;
using OneGate.Backend.Rpc;

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
            await context.MarshallWith(_service.CreateOhlcTimeseries);
        }

        public async Task Consume(ConsumeContext<GetOhlcTimeseries> context)
        {
            await context.MarshallWith(_service.GetOhlcTimeseriess);
        }

        public async Task Consume(ConsumeContext<DeleteOhlcTimeseries> context)
        {
            await context.MarshallWith(_service.DeleteOhlcTimeseries);
        }

        public async Task Consume(ConsumeContext<CreateValueTimeseries> context)
        {
            await context.MarshallWith(_service.CreateValueTimeseries);
        }

        public async Task Consume(ConsumeContext<GetValueTimeseries> context)
        {
            await context.MarshallWith(_service.GetValueTimeseries);
        }

        public async Task Consume(ConsumeContext<DeleteValueTimeseries> context)
        {
            await context.MarshallWith(_service.DeleteValueTimeseries);
        }
    }
}