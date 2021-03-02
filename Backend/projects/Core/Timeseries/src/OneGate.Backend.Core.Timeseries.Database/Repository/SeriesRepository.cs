using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Core.Shared.Database.Repository;
using OneGate.Backend.Core.Timeseries.Database.Models;

namespace OneGate.Backend.Core.Timeseries.Database.Repository
{
    public class SeriesRepository : GenericRepository<Series>, ISeriesRepository
    {
        private readonly DatabaseContext _db;

        public SeriesRepository(DatabaseContext db) : base(db, db.Series)
        {
            _db = db;
        }

        public async Task<IEnumerable<Series>> AddOrUpdateAsync(IEnumerable<Series> entity, DateTime createdAt = default)
        {
            var seriesRange = entity.ToArray();
            var lastUpdate = (createdAt == default) ? DateTime.Now : createdAt;
            
            await _db.Series
                .UpsertRange(seriesRange)
                .On(x => new
                {
                    x.LayerId,
                    x.Interval,
                    x.Timestamp
                })
                .UpdateIf(p => p.LastUpdate < lastUpdate)
                .RunAsync();
            
            return seriesRange;
        }
    }
}