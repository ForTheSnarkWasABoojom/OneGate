using System.Threading.Tasks;
using OneGate.Shared.Models.Series.Point;

namespace OneGate.Backend.Services.TimeseriesService.Repository
{
    public interface IPointSeriesRepository
    {
        public Task AddAsync(PointSeriesDto request);
        public Task<PointSeriesDto> FilterAsync(PointSeriesFilterDto request);
        public Task RemoveAsync(PointSeriesFilterDto request);
    }
}