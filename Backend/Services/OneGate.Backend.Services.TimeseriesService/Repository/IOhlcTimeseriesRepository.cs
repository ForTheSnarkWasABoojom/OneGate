using System.Threading.Tasks;
using OneGate.Backend.Contracts.Common;
using OneGate.Backend.Contracts.Timeseries;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Services.TimeseriesService.Repository
{
    public interface IOhlcTimeseriesRepository
    {
        public Task AddRangeAsync(OhlcTimeseriesRangeDto model);
        public Task UpsertRangeAsync(OhlcTimeseriesRangeDto model);
        public Task<OhlcTimeseriesRangeDto> FilterAsync(OhlcTimeseriesFilterDto model);
        public Task RemoveRangeAsync(OhlcTimeseriesFilterDto model);
    }
}