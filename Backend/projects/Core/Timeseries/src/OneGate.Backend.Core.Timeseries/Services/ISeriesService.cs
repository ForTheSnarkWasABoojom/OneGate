using System.Threading.Tasks;
using OneGate.Backend.Core.Timeseries.Contracts.Series;

namespace OneGate.Backend.Core.Timeseries.Services
{
    public interface ISeriesService
    {
        public Task<SeriesResponse> GetSeriesAsync(GetSeries request);
    }
}