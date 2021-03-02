using System.Threading.Tasks;
using OneGate.Backend.Transport.Contracts.Timeseries;

namespace OneGate.Backend.Core.Timeseries.Worker.Services
{
    public interface ISeriesService
    {
        public Task UpdateMarketDataAsync(MarketDataUpdated request);
    }
}