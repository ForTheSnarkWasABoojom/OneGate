using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Database;
using OneGate.Backend.Database.Models;
using OneGate.Common.Models.Series.Point;

namespace OneGate.Backend.Core.SeriesService.Repository
{
    public class PointSeriesRepository : IPointSeriesRepository
    {
        private readonly DatabaseContext _db;

        public PointSeriesRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task AddAsync(PointSeriesDto request)
        {
            await _db.PointSeries.AddRangeAsync(request.Range.Select(value =>
                new PointSeries
                {
                    LayoutId = request.LayoutId,
                    AssetId = request.AssetId,
                    Timestamp = value.Timestamp,
                    Value = value.Value
                }));
            await _db.SaveChangesAsync();
        }

        public async Task<PointSeriesDto> FilterAsync(
            PointSeriesFilterDto filter)
        {
            var query = _db.PointSeries
                .Where(x => x.AssetId == filter.AssetId)
                .Where(x => x.LayoutId == filter.LayoutId);

            if (filter.Id != null)
                query = query.Where(x => x.Id == filter.Id);
            
            if (filter.StartTimestamp != null)
                query = query.Where(x => x.Timestamp >= filter.StartTimestamp);

            if (filter.EndTimestamp != null)
                query = query.Where(x => x.Timestamp <= filter.EndTimestamp);

            var pointTimeseries = await query.Skip(filter.Shift).Take(filter.Count).ToListAsync();
            return new PointSeriesDto
            {
                LayoutId = filter.LayoutId,
                AssetId = filter.AssetId,
                Range = pointTimeseries.Select(ConvertPointToDto).ToList()
            };
        }

        public async Task RemoveAsync(PointSeriesFilterDto request)
        {
            var query = _db.PointSeries
                .Where(x => x.AssetId == request.AssetId)
                .Where(x => x.LayoutId == request.LayoutId);

            if (request.StartTimestamp != null)
                query = query.Where(x => x.Timestamp >= request.StartTimestamp);

            if (request.EndTimestamp != null)
                query = query.Where(x => x.Timestamp <= request.EndTimestamp);

            var queryTimeseries = await query.Skip(request.Shift).Take(request.Count).ToListAsync();

            _db.PointSeries.RemoveRange(queryTimeseries);
            await _db.SaveChangesAsync();
        }

        private static PointDto ConvertPointToDto(PointSeries model)
        {
            return new PointDto
            {
                Value = model.Value,
                Timestamp = model.Timestamp
            };
        }
    }
}