using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Database;
using OneGate.Backend.Database.Models;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Services.TimeseriesService.Repository
{
    public class OhlcTimeseriesRepository : IOhlcTimeseriesRepository
    {
        private readonly DatabaseContext _db;

        public OhlcTimeseriesRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task AddRangeAsync(OhlcTimeseriesRangeDto request)
        {
            await _db.OhlcTimeseries.AddRangeAsync(request.Range.Select(ohlc => new OhlcTimeseries
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
        
        public async Task UpsertRangeAsync(OhlcTimeseriesRangeDto request)
        {
            foreach (var ohlcDto in request.Range)
            {
                await _db.OhlcTimeseries
                    .Upsert(new OhlcTimeseries
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
                    .WhenMatched(x => new OhlcTimeseries
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

        public async Task<OhlcTimeseriesRangeDto> FilterAsync(OhlcTimeseriesFilterDto filter)
        {
            var query = _db.OhlcTimeseries
                .Where(x => x.Interval == filter.Interval.ToString())
                .Where(x => x.AssetId == filter.AssetId);

            if (filter.Id != null)
                query = query.Where(x => x.Id == filter.Id);
            
            if (filter.StartTimestamp != null)
                query = query.Where(x => x.Timestamp >= filter.StartTimestamp);

            if (filter.EndTimestamp != null)
                query = query.Where(x => x.Timestamp <= filter.EndTimestamp);

            var queryResult = await query.Skip(filter.Shift).Take(filter.Count).ToListAsync();
            return new OhlcTimeseriesRangeDto
            {
                Interval = filter.Interval,
                AssetId = filter.AssetId,
                Range = queryResult.Select(ConvertOhlcToDto).ToList()
            };
        }

        public async Task RemoveRangeAsync(OhlcTimeseriesFilterDto request)
        {
            var query = _db.OhlcTimeseries
                .Where(x => x.Interval == request.Interval.ToString())
                .Where(x => x.AssetId == request.AssetId);

            if (request.StartTimestamp != null)
                query = query.Where(x => x.Timestamp >= request.StartTimestamp);

            if (request.EndTimestamp != null)
                query = query.Where(x => x.Timestamp <= request.EndTimestamp);

            var queryResult = await query.Skip(request.Shift).Take(request.Count).ToListAsync();
            _db.OhlcTimeseries.RemoveRange(queryResult);
            await _db.SaveChangesAsync();
        }

        private static OhlcTimeseriesDto ConvertOhlcToDto(OhlcTimeseries model)
        {
            return new OhlcTimeseriesDto
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