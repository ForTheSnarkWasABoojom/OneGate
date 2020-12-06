using System.Threading.Tasks;
using OneGate.Shared.Models.Series.Ohlc;

namespace OneGate.Backend.Services.TimeseriesService.Repository
{
    public interface IOhlcSeriesRepository
    {
        public Task AddAsync(OhlcSeriesDto model);
        public Task UpsertAsync(OhlcSeriesDto model);
        public Task<OhlcSeriesDto> FilterAsync(OhlcSeriesFilterDto model);
        public Task RemoveAsync(OhlcSeriesFilterDto model);
    }
}