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
using OneGate.Backend.Rpc.Contracts.Ohlc.CreateOhlcs;
using OneGate.Backend.Rpc.Contracts.Ohlc.DeleteOhlcs;
using OneGate.Backend.Rpc.Contracts.Ohlc.GetOhlcsByFilter;
using OneGate.Backend.Rpc.Services;
using OneGate.Shared.Models.Ohlc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace OneGate.Backend.OhlcService
{
    public class OhlcService : IHostedService, IOhlcService
    {
        private readonly ILogger<OhlcService> _logger;
        private readonly IBus _bus;

        public OhlcService(ILogger<OhlcService> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;

            // Register method as remote callable.
            _bus.RegisterMethodAsync<HealthCheckRequest, HealthCheckResponse>(HealthCheckAsync);
            _bus.RegisterMethodAsync<CreateOhlcsRequest, CreateOhlcsResponse>(CreateOhlcsAsync);
            _bus.RegisterMethodAsync<GetOhlcsByFilterRequest, GetOhlcsByFilterResponse>(GetOhlcsByFilterAsync);
            _bus.RegisterMethodAsync<DeleteOhlcsRequest, DeleteOhlcsResponse>(DeleteOhlcsAsync);
        }

        public async Task<HealthCheckResponse> HealthCheckAsync(HealthCheckRequest request)
        {
            return new HealthCheckResponse
            {
                Timestamp = DateTime.Now
            };
        }

        public async Task<CreateOhlcsResponse> CreateOhlcsAsync(CreateOhlcsRequest request)
        {
            await using var db = new DatabaseContext();
            
            if (request.OhlcRange.Range.GroupBy(x => x.Timestamp).Count() != request.OhlcRange.Range.Count)
                throw new ApiException("OHLC range must be unique", Status400BadRequest);

            if (!await db.Assets.AnyAsync(x => x.Id == request.OhlcRange.AssetId))
                throw new ApiException("Asset with specified id does not exist", Status404NotFound);

            foreach (var ohlc in request.OhlcRange.Range)
            {
                if (await db.Ohlcs.AnyAsync(x =>
                    request.OhlcRange.AssetId == x.AssetId && ohlc.Timestamp == x.Timestamp &&
                    request.OhlcRange.Interval.ToString() == x.Interval))
                    throw new ApiException("OHLC range must be unique", Status400BadRequest);
            }

            await db.Ohlcs.AddRangeAsync(request.OhlcRange.Range.Select(ohlc => new Ohlc
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

            return new CreateOhlcsResponse
            {
                OhlcRange = new OhlcRangeDto
                {
                    Interval = request.OhlcRange.Interval,
                    AssetId = request.OhlcRange.AssetId,
                    Range = request.OhlcRange.Range.Select(x =>
                        new OhlcDto
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

        public async Task<GetOhlcsByFilterResponse> GetOhlcsByFilterAsync(GetOhlcsByFilterRequest request)
        {
            await using var db = new DatabaseContext();

            var ohlcsQuery = db.Ohlcs.AsQueryable();
            ohlcsQuery = ohlcsQuery.Where(x => x.Interval == request.Filter.Interval.ToString());
            ohlcsQuery = ohlcsQuery.Where(x => x.AssetId == request.Filter.AssetId);

            if (request.Filter.StartTimestamp != null)
                ohlcsQuery = ohlcsQuery.Where(x => x.Timestamp >= request.Filter.StartTimestamp);

            if (request.Filter.EndTimestamp != null)
                ohlcsQuery = ohlcsQuery.Where(x => x.Timestamp <= request.Filter.EndTimestamp);

            var ohlcs = await ohlcsQuery.Skip(request.Filter.Shift).Take(request.Filter.Count).ToListAsync();
            return new GetOhlcsByFilterResponse
            {
                OhlcRange = new OhlcRangeDto
                {
                    Interval = request.Filter.Interval,
                    AssetId = request.Filter.AssetId,
                    Range = ohlcs.Select(ConvertOhlcToDto).ToList()
                }
            };
        }

        public async Task<DeleteOhlcsResponse> DeleteOhlcsAsync(DeleteOhlcsRequest request)
        {
            await using var db = new DatabaseContext();

            var ohlcsQuery = db.Ohlcs.AsQueryable();
            ohlcsQuery = ohlcsQuery.Where(x => x.Interval == request.Filter.Interval.ToString());
            ohlcsQuery = ohlcsQuery.Where(x => x.AssetId == request.Filter.AssetId);

            if (request.Filter.StartTimestamp != null)
                ohlcsQuery = ohlcsQuery.Where(x => x.Timestamp >= request.Filter.StartTimestamp);

            if (request.Filter.EndTimestamp != null)
                ohlcsQuery = ohlcsQuery.Where(x => x.Timestamp <= request.Filter.EndTimestamp);

            db.Ohlcs.RemoveRange(ohlcsQuery);
            await db.SaveChangesAsync();

            return new DeleteOhlcsResponse();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Account service started");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Account service stopped");
        }

        private OhlcDto ConvertOhlcToDto(Ohlc ohlc)
        {
            return new OhlcDto
            {
                Low = ohlc.Low,
                High = ohlc.High,
                Open = ohlc.Open,
                Close = ohlc.Close,
                Timestamp = ohlc.Timestamp,
            };
        }
    }
}