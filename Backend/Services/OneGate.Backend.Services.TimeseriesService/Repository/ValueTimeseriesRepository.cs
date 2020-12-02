using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Database;
using OneGate.Backend.Database.Models;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Services.TimeseriesService.Repository
{
    public class ValueTimeseriesRepository : IValueTimeseriesRepository
    {
        private readonly DatabaseContext _db;

        public ValueTimeseriesRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task AddRangeAsync(ValueTimeseriesRangeDto request)
        {
            await _db.ValueTimeseries.AddRangeAsync(request.Range.Select(value =>
                new ValueTimeseries
                {
                    LayoutId = request.LayoutId,
                    AssetId = request.AssetId,
                    Timestamp = value.Timestamp,
                    Value = value.Value
                }));
            await _db.SaveChangesAsync();
        }

        public async Task<ValueTimeseriesRangeDto> FilterAsync(
            ValueTimeseriesFilterDto filter)
        {
            var query = _db.ValueTimeseries
                .Where(x => x.AssetId == filter.AssetId)
                .Where(x => x.LayoutId == filter.LayoutId);

            if (filter.Id != null)
                query = query.Where(x => x.Id == filter.Id);
            
            if (filter.StartTimestamp != null)
                query = query.Where(x => x.Timestamp >= filter.StartTimestamp);

            if (filter.EndTimestamp != null)
                query = query.Where(x => x.Timestamp <= filter.EndTimestamp);

            var valueTimeseries = await query.Skip(filter.Shift).Take(filter.Count).ToListAsync();
            return new ValueTimeseriesRangeDto
            {
                LayoutId = filter.LayoutId,
                AssetId = filter.AssetId,
                Range = valueTimeseries.Select(ConvertValueTimeseriesToDto).ToList()
            };
        }

        public async Task RemoveRangeAsync(ValueTimeseriesFilterDto request)
        {
            var query = _db.ValueTimeseries
                .Where(x => x.AssetId == request.AssetId)
                .Where(x => x.LayoutId == request.LayoutId);

            if (request.StartTimestamp != null)
                query = query.Where(x => x.Timestamp >= request.StartTimestamp);

            if (request.EndTimestamp != null)
                query = query.Where(x => x.Timestamp <= request.EndTimestamp);

            var queryTimeseries = await query.Skip(request.Shift).Take(request.Count).ToListAsync();

            _db.ValueTimeseries.RemoveRange(queryTimeseries);
            await _db.SaveChangesAsync();
        }

        private static ValueTimeseriesDto ConvertValueTimeseriesToDto(ValueTimeseries model)
        {
            return new ValueTimeseriesDto
            {
                Value = model.Value,
                Timestamp = model.Timestamp
            };
        }
    }
}