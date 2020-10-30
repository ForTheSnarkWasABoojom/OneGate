using System.Threading.Tasks;
using OneGate.Backend.Contracts.Common;
using OneGate.Backend.Contracts.Timeseries;
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