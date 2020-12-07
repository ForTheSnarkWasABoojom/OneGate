using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Database;
using OneGate.Backend.Database.Models;
using OneGate.Common.Models.Series.Ohlc;

namespace OneGate.Backend.Core.SeriesService.Repository
{
    public class OhlcSeriesRepository : IOhlcSeriesRepository
    {
        private readonly DatabaseContext _db;

        public OhlcSeriesRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task AddAsync(OhlcSeriesDto request)
        {
            await _db.OhlcSeries.AddRangeAsync(request.Range.Select(ohlc => new OhlcSeries
            {
                Low = ohlc.Low,
                High = ohlc.High,
                Open = ohlc.Open,
                Close = ohlc.Close,
                Timestamp = ohlc.Timestamp,
                Interval = request.Interval.ToString(),
                AssetId = request.AssetId,
                LastUpdate = DateTime.Now
            }));
            await _db.SaveChangesAsync();
        }
        
        public async Task UpsertAsync(OhlcSeriesDto request)
        {
            foreach (var ohlcDto in request.Range)
            {
                await _db.OhlcSeries
                    .Upsert(new OhlcSeries
                    {
                        Low = ohlcDto.Low,
                        High = ohlcDto.High,
                        Open = ohlcDto.Open,
                        Close = ohlcDto.Close,
                        Timestamp = ohlcDto.Timestamp,
                        Interval = request.Interval.ToString(),
                        AssetId = request.AssetId,
                        LastUpdate = DateTime.Now
                    })
                    .On(x => new {x.AssetId, x.Interval, x.Timestamp})
                    .WhenMatched(x => new OhlcSeries
                    {
                        Open = ohlcDto.Open,
                        Close = ohlcDto.Close,
                        Low = ohlcDto.Low,
                        High = ohlcDto.High,
                        LastUpdate = DateTime.Now
                    })
                    .RunAsync();
            }
        }

        public async Task<OhlcSeriesDto> FilterAsync(OhlcSeriesFilterDto filter)
        {
            var query = _db.OhlcSeries
                .Where(x => x.Interval == filter.Interval.ToString())
                .Where(x => x.AssetId == filter.AssetId);

            if (filter.Id != null)
                query = query.Where(x => x.Id == filter.Id);
            
            if (filter.StartTimestamp != null)
                query = query.Where(x => x.Timestamp >= filter.StartTimestamp);

            if (filter.EndTimestamp != null)
                query = query.Where(x => x.Timestamp <= filter.EndTimestamp);

            var queryResult = await query.Skip(filter.Shift).Take(filter.Count).ToListAsync();
            return new OhlcSeriesDto
            {
                Interval = filter.Interval,
                AssetId = filter.AssetId,
                Range = queryResult.Select(ConvertOhlcItemToDto).ToList()
            };
        }

        public async Task RemoveAsync(OhlcSeriesFilterDto request)
        {
            var query = _db.OhlcSeries
                .Where(x => x.Interval == request.Interval.ToString())
                .Where(x => x.AssetId == request.AssetId);

            if (request.StartTimestamp != null)
                query = query.Where(x => x.Timestamp >= request.StartTimestamp);

            if (request.EndTimestamp != null)
                query = query.Where(x => x.Timestamp <= request.EndTimestamp);

            var queryResult = await query.Skip(request.Shift).Take(request.Count).ToListAsync();
            _db.OhlcSeries.RemoveRange(queryResult);
            await _db.SaveChangesAsync();
        }

        private static OhlcDto ConvertOhlcItemToDto(OhlcSeries model)
        {
            return new OhlcDto
            {
                Low = model.Low,
                High = model.High,
                Open = model.Open,
                Close = model.Close,
                Timestamp = model.Timestamp,
            };
        }
    }
}