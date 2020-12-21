using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Core.Timeseries.Database.Models;

namespace OneGate.Backend.Core.Timeseries.Database.Repository
{
    public class PointSeriesRepository : IPointSeriesRepository
    {
        private readonly DatabaseContext _db;

        public PointSeriesRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task AddAsync(IEnumerable<PointSeries> series)
        {
            await _db.PointSeries.AddRangeAsync(series);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<PointSeries>> FilterAsync(
            int? id, int layoutId, int assetId,
            DateTime? endTimestamp, DateTime? startTimestamp, int shift, int count)
        {
            var query = _db.PointSeries
                .Where(x => x.AssetId == assetId)
                .Where(x => x.LayoutId == layoutId);

            if (id != null)
                query = query.Where(x => x.Id == id);

            if (startTimestamp != null)
                query = query.Where(x => x.Timestamp >= startTimestamp);

            if (endTimestamp != null)
                query = query.Where(x => x.Timestamp <= endTimestamp);

            return await query.Skip(shift).Take(count).ToListAsync();
        }

        public async Task RemoveAsync(int layoutId, int assetId,
            DateTime? endTimestamp, DateTime? startTimestamp, int shift, int count)
        {
            var query = _db.PointSeries
                .Where(x => x.AssetId == assetId)
                .Where(x => x.LayoutId == layoutId);

            if (startTimestamp != null)
                query = query.Where(x => x.Timestamp >= startTimestamp);

            if (endTimestamp != null)
                query = query.Where(x => x.Timestamp <= endTimestamp);

            var queryTimeseries = await query.Skip(shift).Take(count).ToListAsync();

            _db.PointSeries.RemoveRange(queryTimeseries);
            await _db.SaveChangesAsync();
        }
    }
}