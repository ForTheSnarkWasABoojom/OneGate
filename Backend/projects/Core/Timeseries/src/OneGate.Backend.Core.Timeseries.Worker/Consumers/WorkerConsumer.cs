using System.Threading.Tasks;
using MassTransit;
using OneGate.Backend.Core.Timeseries.Worker.Services;
using OneGate.Backend.Transport.Contracts.Timeseries;

namespace OneGate.Backend.Core.Timeseries.Worker.Consumers
{
    public class WorkerConsumer : IConsumer<MarketDataUpdated>
    {
        private readonly ISeriesService _seriesService;

        public WorkerConsumer(ISeriesService seriesService)
        {
            _seriesService = seriesService;
        }

        public async Task Consume(ConsumeContext<MarketDataUpdated> context)
        {
            await _seriesService.UpdateMarketDataAsync(context.Message);
        }
    }
}