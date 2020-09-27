using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Database;
using OneGate.Backend.Database.Models;
using OneGate.Backend.Rpc;
using OneGate.Backend.Rpc.Contracts.Base.HealthCheck;
using OneGate.Backend.Rpc.Contracts.Timeseries.CreateOhlcTimeseries;
using OneGate.Backend.Rpc.Contracts.Timeseries.CreateValueTimeseries;
using OneGate.Backend.Rpc.Contracts.Timeseries.DeleteOhlcTimeseries;
using OneGate.Backend.Rpc.Contracts.Timeseries.DeleteValueTimeseries;
using OneGate.Backend.Rpc.Contracts.Timeseries.GetOhlcTimeseriesByFilter;
using OneGate.Backend.Rpc.Contracts.Timeseries.GetValueTimeseriesByFilter;
using OneGate.Backend.Rpc.Services;
using OneGate.Shared.Models.Timeseries;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace OneGate.Backend.TimeseriesService
{
    public class TimeseriesService : IHostedService, ITimeseriesService
    {
        private readonly ILogger<TimeseriesService> _logger;
        private readonly IBus _bus;

        public TimeseriesService(ILogger<TimeseriesService> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;

            // Register method as remote callable.
            _bus.RegisterMethodAsync<HealthCheckRequest, HealthCheckResponse>(HealthCheckAsync);
            _bus.RegisterMethodAsync<CreateOhlcTimeseriesRequest, CreateOhlcTimeseriesResponse>(
                CreateOhlcTimeseriesAsync);
            _bus.RegisterMethodAsync<GetOhlcTimeseriesByFilterRequest, GetOhlcTimeseriesByFilterResponse>(
                GetOhlcTimeseriesByFilterAsync);
            _bus.RegisterMethodAsync<DeleteOhlcTimeseriesRequest, DeleteOhlcTimeseriesResponse>(
                DeleteOhlcTimeseriesAsync);

            _bus.RegisterMethodAsync<CreateValueTimeseriesRequest, CreateValueTimeseriesResponse>(
                CreateValueTimeseriesAsync);
            _bus.RegisterMethodAsync<GetValueTimeseriesByFilterRequest, GetValueTimeseriesByFilterResponse>(
                GetValueTimeseriesByFilterAsync);
            _bus.RegisterMethodAsync<DeleteValueTimeseriesRequest, DeleteValueTimeseriesResponse>(
                DeleteValueTimeseriesAsync);
        }

        public async Task<HealthCheckResponse> HealthCheckAsync(HealthCheckRequest request)
        {
            return new HealthCheckResponse
            {
                Timestamp = DateTime.Now
            };
        }

        public async Task<CreateOhlcTimeseriesResponse> CreateOhlcTimeseriesAsync(CreateOhlcTimeseriesRequest request)
        {
            await using var db = new DatabaseContext();

            if (request.OhlcRange.Range.GroupBy(x => x.Timestamp).Count() != request.OhlcRange.Range.Count)
                throw new ApiException("OHLC range must be unique", Status400BadRequest);

            if (!await db.Assets.AnyAsync(x => x.Id == request.OhlcRange.AssetId))
                throw new ApiException("Asset with specified id does not exist", Status404NotFound);

            foreach (var ohlc in request.OhlcRange.Range)
            {
                if (await db.OhlcTimeseries.AnyAsync(x =>
                    request.OhlcRange.AssetId == x.AssetId &&
                    ohlc.Timestamp == x.Timestamp &&
                    request.OhlcRange.Interval.ToString() == x.Interval))
                    throw new ApiException("OHLC range must be unique", Status400BadRequest);
            }

            await db.OhlcTimeseries.AddRangeAsync(request.OhlcRange.Range.Select(ohlc => new OhlcTimeseries
            {
                Low = ohlc.Low,
                High = ohlc.High,
                Open = ohlc.Open,
                Close = ohlc.Close,
                Timestamp = ohlc.Timestamp,
                Interval = request.OhlcRange.Interval.ToString(),
                AssetId = request.OhlcRange.AssetId
            }));
            await db.SaveChangesAsync();

            return new CreateOhlcTimeseriesResponse
            {
                OhlcRange = new OhlcTimeseriesRangeDto
                {
                    Interval = request.OhlcRange.Interval,
                    AssetId = request.OhlcRange.AssetId,
                    Range = request.OhlcRange.Range.Select(x =>
                        new OhlcTimeseriesDto
                        {
                            Low = x.Low,
                            High = x.High,
                            Open = x.Open,
                            Close = x.Close
                        }
                    ).ToList()
                }
            };
        }

        public async Task<GetOhlcTimeseriesByFilterResponse> GetOhlcTimeseriesByFilterAsync(
            GetOhlcTimeseriesByFilterRequest request)
        {
            await using var db = new DatabaseContext();

            var query = db.OhlcTimeseries
                .Where(x => x.Interval == request.Filter.Interval.ToString())
                .Where(x => x.AssetId == request.Filter.AssetId);

            if (request.Filter.StartTimestamp != null)
                query = query.Where(x => x.Timestamp >= request.Filter.StartTimestamp);

            if (request.Filter.EndTimestamp != null)
                query = query.Where(x => x.Timestamp <= request.Filter.EndTimestamp);

            var ohlcTimeseries = await query.Skip(request.Filter.Shift).Take(request.Filter.Count).ToListAsync();
            return new GetOhlcTimeseriesByFilterResponse
            {
                OhlcRange = new OhlcTimeseriesRangeDto
                {
                    Interval = request.Filter.Interval,
                    AssetId = request.Filter.AssetId,
                    Range = ohlcTimeseries.Select(ConvertOhlcToDto).ToList()
                }
            };
        }

        public async Task<DeleteOhlcTimeseriesResponse> DeleteOhlcTimeseriesAsync(DeleteOhlcTimeseriesRequest request)
        {
            await using var db = new DatabaseContext();

            var query = db.OhlcTimeseries
                .Where(x => x.Interval == request.Filter.Interval.ToString())
                .Where(x => x.AssetId == request.Filter.AssetId);

            if (request.Filter.StartTimestamp != null)
                query = query.Where(x => x.Timestamp >= request.Filter.StartTimestamp);

            if (request.Filter.EndTimestamp != null)
                query = query.Where(x => x.Timestamp <= request.Filter.EndTimestamp);
            
            var queryTimeseries = await query.Skip(request.Filter.Shift).Take(request.Filter.Count).ToListAsync();
            
            db.OhlcTimeseries.RemoveRange(queryTimeseries);
            await db.SaveChangesAsync();

            return new DeleteOhlcTimeseriesResponse();
        }

        public async Task<CreateValueTimeseriesResponse> CreateValueTimeseriesAsync(
            CreateValueTimeseriesRequest request)
        {
            await using var db = new DatabaseContext();

            if (request.ValueTimeseriesRange.Range.GroupBy(x => x.Timestamp).Count() !=
                request.ValueTimeseriesRange.Range.Count)
                throw new ApiException("Value timeseries range must be unique", Status400BadRequest);

            if (!await db.Assets.AnyAsync(x => x.Id == request.ValueTimeseriesRange.AssetId))
                throw new ApiException("Asset with specified id does not exist", Status404NotFound);

            foreach (var value in request.ValueTimeseriesRange.Range)
            {
                if (await db.ValueTimeseries.AnyAsync(x =>
                    request.ValueTimeseriesRange.AssetId == x.AssetId &&
                    request.ValueTimeseriesRange.Name == x.Name &&
                    value.Timestamp == x.Timestamp))
                    throw new ApiException("Value timeseries range must be unique", Status400BadRequest);
            }

            await db.ValueTimeseries.AddRangeAsync(request.ValueTimeseriesRange.Range.Select(value =>
                new ValueTimeseries
                {
                    Name = request.ValueTimeseriesRange.Name,
                    AssetId = request.ValueTimeseriesRange.AssetId,
                    Timestamp = value.Timestamp,
                    Value = value.Value
                }));
            await db.SaveChangesAsync();

            return new CreateValueTimeseriesResponse
            {
                ValueTimeseriesRange = new ValueTimeseriesRangeDto
                {
                    Name = request.ValueTimeseriesRange.Name,
                    AssetId = request.ValueTimeseriesRange.AssetId,
                    Range = request.ValueTimeseriesRange.Range.Select(x =>
                        new ValueTimeseriesDto
                        {
                            Value = x.Value,
                            Timestamp = x.Timestamp
                        }
                    ).ToList()
                }
            };
        }

        public async Task<GetValueTimeseriesByFilterResponse> GetValueTimeseriesByFilterAsync(
            GetValueTimeseriesByFilterRequest request)
        {
            await using var db = new DatabaseContext();

            var query = db.ValueTimeseries
                .Where(x => x.AssetId == request.Filter.AssetId)
                .Where(x => x.Name == request.Filter.Name);

            if (request.Filter.StartTimestamp != null)
                query = query.Where(x => x.Timestamp >= request.Filter.StartTimestamp);

            if (request.Filter.EndTimestamp != null)
                query = query.Where(x => x.Timestamp <= request.Filter.EndTimestamp);

            var valueTimeseries = await query.Skip(request.Filter.Shift).Take(request.Filter.Count).ToListAsync();
            return new GetValueTimeseriesByFilterResponse
            {
                ValueTimeseriesRange = new ValueTimeseriesRangeDto
                {
                    Name = request.Filter.Name,
                    AssetId = request.Filter.AssetId,
                    Range = valueTimeseries.Select(ConvertValueTimeseriesToDto).ToList()
                }
            };
        }

        public async Task<DeleteValueTimeseriesResponse> DeleteValueTimeseriesAsync(
            DeleteValueTimeseriesRequest request)
        {
            await using var db = new DatabaseContext();

            var query = db.ValueTimeseries
                .Where(x => x.AssetId == request.Filter.AssetId)
                .Where(x => x.Name == request.Filter.Name);

            if (request.Filter.StartTimestamp != null)
                query = query.Where(x => x.Timestamp >= request.Filter.StartTimestamp);

            if (request.Filter.EndTimestamp != null)
                query = query.Where(x => x.Timestamp <= request.Filter.EndTimestamp);
            
            var queryTimeseries = await query.Skip(request.Filter.Shift).Take(request.Filter.Count).ToListAsync();
          
            db.ValueTimeseries.RemoveRange(queryTimeseries);
            await db.SaveChangesAsync();

            return new DeleteValueTimeseriesResponse();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Account service started");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Account service stopped");
        }

        private OhlcTimeseriesDto ConvertOhlcToDto(OhlcTimeseries model)
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

        private ValueTimeseriesDto ConvertValueTimeseriesToDto(ValueTimeseries model)
        {
            return new ValueTimeseriesDto
            {
                Value = model.Value,
                Timestamp = model.Timestamp
            };
        }
    }
}