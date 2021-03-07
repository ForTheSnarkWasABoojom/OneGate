using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Core.Shared.Database.Repository;
using OneGate.Backend.Core.Timeseries.Database.Models;

namespace OneGate.Backend.Core.Timeseries.Database.Repository
{
    public class OhlcRepository : GenericRepository<Ohlc>, IOhlcRepository
    {
        private readonly DatabaseContext _db;

        public OhlcRepository(DatabaseContext db) : base(db, db.Ohlcs)
        {
            _db = db;
        }

        public async Task<List<Ohlc>> AddOrUpdateRangeAsync(IEnumerable<Ohlc> entity, DateTime createdAt = default)
        {
            var seriesRange = entity.ToList();
            var lastUpdate = (createdAt == default) ? DateTime.Now : createdAt;

            await _db.Ohlcs
                .UpsertRange(seriesRange)
                .On(x => new
                {
                    x.AssetId,
                    x.Interval,
                    x.Timestamp
                })
                .UpdateIf(p => p.LastUpdate < lastUpdate)
                .RunAsync();

            return seriesRange;
        }
    }
}