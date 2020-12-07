using System.Threading.Tasks;
using OneGate.Common.Models.Series.Point;

namespace OneGate.Backend.Core.SeriesService.Repository
{
    public interface IPointSeriesRepository
    {
        public Task AddAsync(PointSeriesDto request);
        public Task<PointSeriesDto> FilterAsync(PointSeriesFilterDto request);
        public Task RemoveAsync(PointSeriesFilterDto request);
    }
}