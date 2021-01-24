using System.Threading.Tasks;
using MassTransit;
using OneGate.Backend.Core.Timeseries.Services;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Timeseries.Ohlc;
using OneGate.Backend.Transport.Contracts.Timeseries.Point;

namespace OneGate.Backend.Core.Timeseries.Consumers
{
    public class Worker :
        IConsumer<GetOhlcSeries>,
        IConsumer<GetPointSeries>
    {
        private readonly IService _service;

        public Worker(IService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<GetOhlcSeries> context)
        {
            await context.MarshallWith(_service.GetOhlcSeriesAsync);
        }

        public async Task Consume(ConsumeContext<GetPointSeries> context)
        {
            await context.MarshallWith(_service.GetPointSeriesAsync);
        }
    }
}