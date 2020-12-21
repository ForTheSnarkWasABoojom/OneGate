using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Core.Timeseries.Database.Models;

namespace OneGate.Backend.Core.Timeseries.Database.Repository
{
    public class OhlcSeriesRepository : IOhlcSeriesRepository
    {
        private readonly DatabaseContext _db;

        public OhlcSeriesRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task AddAsync(IEnumerable<OhlcSeries> request)
        {
            await _db.OhlcSeries.AddRangeAsync(request);
            await _db.SaveChangesAsync();
        }

        public async Task UpsertAsync(OhlcSeries request)
        {
            await _db.OhlcSeries.Upsert(new OhlcSeries
                {
                    Low = request.Low,
                    High = request.High,
                    Open = request.Open,
                    Close = request.Close,
                    Timestamp = request.Timestamp,
                    Interval = request.Interval,
                    AssetId = request.AssetId,
                    LastUpdate = DateTime.Now
                })
                .On(x => new {x.AssetId, x.Interval, x.Timestamp})
                .WhenMatched(x => new OhlcSeries
                {
                    Open = request.Open,
                    Close = request.Close,
                    Low = request.Low,
                    High = request.High,
                    LastUpdate = DateTime.Now
                })
                .RunAsync();
        }

        public async Task<IEnumerable<OhlcSeries>> FilterAsync(int? id, string interval, int assetId,
            DateTime? endTimestamp, DateTime? startTimestamp, int shift, int count)
        {
            var query = _db.OhlcSeries
                .Where(x => x.Interval == interval.ToString())
                .Where(x => x.AssetId == assetId);

            if (id != null)
                query = query.Where(x => x.Id == id);

            if (startTimestamp != null)
                query = query.Where(x => x.Timestamp >= startTimestamp);

            if (endTimestamp != null)
                query = query.Where(x => x.Timestamp <= endTimestamp);

            return await query.Skip(shift).Take(count).ToListAsync();
        }

        public async Task RemoveAsync(string interval, int assetId, DateTime? endTimestamp,
            DateTime? startTimestamp, int shift, int count)
        {
            var query = _db.OhlcSeries
                .Where(x => x.Interval == interval.ToString())
                .Where(x => x.AssetId == assetId);

            if (startTimestamp != null)
                query = query.Where(x => x.Timestamp >= startTimestamp);

            if (endTimestamp != null)
                query = query.Where(x => x.Timestamp <= endTimestamp);

            var queryResult = await query.Skip(shift).Take(count).ToListAsync();
            _db.OhlcSeries.RemoveRange(queryResult);
            await _db.SaveChangesAsync();
        }
    }
}