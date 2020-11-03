using System.Threading.Tasks;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Services.TimeseriesService.Repository
{
    public interface IValueTimeseriesRepository
    {
        public Task AddRangeAsync(ValueTimeseriesRangeDto request);
        public Task<ValueTimeseriesRangeDto> FilterAsync(ValueTimeseriesFilterDto request);
        public Task RemoveRangeAsync(ValueTimeseriesFilterDto request);
    }
}