using System.Threading.Tasks;
using MassTransit;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Series.Ohlc;
using OneGate.Backend.Transport.Contracts.Series.Point;

namespace OneGate.Backend.Core.SeriesService
{
    public class Consumer :
        IConsumer<OnOhlcSeriesUpdated>,
        IConsumer<CreateOhlcSeries>,
        IConsumer<GetOhlcSeries>,
        IConsumer<DeleteOhlcSeries>,
        IConsumer<CreatePointSeries>,
        IConsumer<GetPointSeries>,
        IConsumer<DeletePointSeries>
    {
        private readonly IService _service;

        public Consumer(IService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<OnOhlcSeriesUpdated> context)
        {
            await _service.OnOhlcSeriesUpdated(context.Message);
        }

        public async Task Consume(ConsumeContext<CreateOhlcSeries> context)
        {
            await context.MarshallWith(_service.CreateOhlcSeries);
        }

        public async Task Consume(ConsumeContext<GetOhlcSeries> context)
        {
            await context.MarshallWith(_service.GetOhlcSeries);
        }

        public async Task Consume(ConsumeContext<DeleteOhlcSeries> context)
        {
            await context.MarshallWith(_service.DeleteOhlcSeries);
        }

        public async Task Consume(ConsumeContext<CreatePointSeries> context)
        {
            await context.MarshallWith(_service.CreatePointSeries);
        }

        public async Task Consume(ConsumeContext<GetPointSeries> context)
        {
            await context.MarshallWith(_service.GetPointSeries);
        }

        public async Task Consume(ConsumeContext<DeletePointSeries> context)
        {
            await context.MarshallWith(_service.DeletePointSeries);
        }
    }
}