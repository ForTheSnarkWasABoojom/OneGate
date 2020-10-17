using System;
using System.Collections.Generic;
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
using OneGate.Backend.Rpc.Contracts.Timeseries;
using OneGate.Backend.Rpc.Contracts.Timeseries.CreateOhlcTimeseries;
using OneGate.Backend.Rpc.Contracts.Timeseries.CreateValueTimeseries;
using OneGate.Backend.Rpc.Contracts.Timeseries.DeleteOhlcTimeseries;
using OneGate.Backend.Rpc.Contracts.Timeseries.DeleteValueTimeseries;
using OneGate.Backend.Rpc.Contracts.Timeseries.GetOhlcTimeseriesByFilter;
using OneGate.Backend.Rpc.Contracts.Timeseries.GetValueTimeseriesByFilter;
using OneGate.Backend.Rpc.Services;
using OneGate.Shared.Models.Common;
using OneGate.Shared.Models.Timeseries;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace OneGate.Backend.Services.TimeseriesService
{
    public class ExecutorService : IHostedService, ITimeseriesService
    {
        private readonly ILogger<ExecutorService> _logger;
        private readonly IBus _bus;

        public ExecutorService(ILogger<ExecutorService> logger, IBus bus)
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

            // Register subscribers.
            _bus.SubscribeAsync<OnOhlcTimeseriesChanged>("timeseries_service", OhlcTimeseriesChangedHandler);
        }

        private async Task OhlcTimeseriesChangedHandler(OnOhlcTimeseriesChanged model)
        {
            foreach (var (intervalDto, ohlcDto) in model.OhlcByInterval)
            {
                await CreateOrUpdateOhlcTimeseriesAsync(new OhlcTimeseriesRangeDto
                {
                    AssetId = model.AssetId,
                    Interval = intervalDto,
                    Range = new List<OhlcTimeseriesDto>
                    {
                        ohlcDto
                    }
                });
            }
        }

        private async Task CreateOrUpdateOhlcTimeseriesAsync(OhlcTimeseriesRangeDto request,
            DateTime? lastUpdate = null)
        {
            await using var db = new DatabaseContext();

            foreach (var ohlcDto in request.Range)
            {
                await db.OhlcTimeseries
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
                        LastUpdate = lastUpdate ?? DateTime.Now
                    })
                    .RunAsync();
            }
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
            request.OhlcRange.Range = request.OhlcRange.Range.GroupBy(x => x.Timestamp).Select(x => x.First()).ToList();

            await db.OhlcTimeseries.AddRangeAsync(request.OhlcRange.Range.Select(ohlc => new OhlcTimeseries
            {
                Low = ohlc.Low,
                High = ohlc.High,
                Open = ohlc.Open,
                Close = ohlc.Close,
                Timestamp = ohlc.Timestamp,
                Interval = request.OhlcRange.Interval.ToString(),
                AssetId = request.OhlcRange.AssetId,
                LastUpdate = DateTime.Now
            }));
            await db.SaveChangesAsync();

            return new CreateOhlcTimeseriesResponse
            {
                Resource = new ResourceDto()
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
            request.ValueRange.Range =
                request.ValueRange.Range.GroupBy(x => x.Timestamp).Select(x => x.First()).ToList();

            await db.ValueTimeseries.AddRangeAsync(request.ValueRange.Range.Select(value =>
                new ValueTimeseries
                {
                    Name = request.ValueRange.Name,
                    AssetId = request.ValueRange.AssetId,
                    Timestamp = value.Timestamp,
                    Value = value.Value
                }));
            await db.SaveChangesAsync();

            return new CreateValueTimeseriesResponse
            {
                Resource = new ResourceDto()
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
            _logger.LogInformation("Timeseries service started");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timeseries service stopped");
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