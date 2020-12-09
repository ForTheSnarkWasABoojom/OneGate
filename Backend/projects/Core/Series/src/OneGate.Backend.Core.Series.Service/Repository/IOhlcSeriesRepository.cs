using System.Threading.Tasks;
using OneGate.Common.Models.Series.Ohlc;

namespace OneGate.Backend.Core.Series.Service.Repository
{
    public interface IOhlcSeriesRepository
    {
        public Task AddAsync(OhlcSeriesDto model);
        public Task UpsertAsync(OhlcSeriesDto model);
        public Task<OhlcSeriesDto> FilterAsync(OhlcSeriesFilterDto model);
        public Task RemoveAsync(OhlcSeriesFilterDto model);
    }
}