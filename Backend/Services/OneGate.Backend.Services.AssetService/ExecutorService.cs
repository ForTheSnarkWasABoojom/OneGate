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
using OneGate.Backend.Rpc.Contracts.Asset.CreateAsset;
using OneGate.Backend.Rpc.Contracts.Asset.DeleteAsset;
using OneGate.Backend.Rpc.Contracts.Asset.GetAsset;
using OneGate.Backend.Rpc.Contracts.Asset.GetAssetsByFilter;
using OneGate.Backend.Rpc.Contracts.Base.HealthCheck;
using OneGate.Backend.Rpc.Contracts.Exchange.CreateExchange;
using OneGate.Backend.Rpc.Contracts.Exchange.DeleteExchange;
using OneGate.Backend.Rpc.Contracts.Exchange.GetExchange;
using OneGate.Backend.Rpc.Contracts.Exchange.GetExchangesByFilter;
using OneGate.Backend.Rpc.Services;
using OneGate.Shared.Models.Asset;
using OneGate.Shared.Models.Common;
using OneGate.Shared.Models.Exchange;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace OneGate.Backend.Services.AssetService
{
    public class ExecutorService : IHostedService, IAssetService
    {
        private readonly ILogger<ExecutorService> _logger;
        private readonly IBus _bus;

        public ExecutorService(ILogger<ExecutorService> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;

            // Register method as remote callable.
            _bus.RegisterMethodAsync<HealthCheckRequest, HealthCheckResponse>(HealthCheckAsync);

            _bus.RegisterMethodAsync<CreateExchangeRequest, CreateExchangeResponse>(CreateExchangeAsync);
            _bus.RegisterMethodAsync<GetExchangeRequest, GetExchangeResponse>(GetExchangeAsync);
            _bus.RegisterMethodAsync<DeleteExchangeRequest, DeleteExchangeResponse>(DeleteExchangeAsync);
            _bus.RegisterMethodAsync<GetExchangesByFilterRequest, GetExchangesByFilterResponse>(
                GetExchangesByFilterAsync);

            _bus.RegisterMethodAsync<CreateAssetRequest, CreateAssetResponse>(CreateAssetAsync);
            _bus.RegisterMethodAsync<GetAssetRequest, GetAssetResponse>(GetAssetAsync);
            _bus.RegisterMethodAsync<DeleteAssetRequest, DeleteAssetResponse>(DeleteAssetAsync);
            _bus.RegisterMethodAsync<GetAssetsByFilterRequest, GetAssetsByFilterResponse>(GetAssetsByFilterAsync);
        }

        public async Task<HealthCheckResponse> HealthCheckAsync(HealthCheckRequest request)
        {
            return new HealthCheckResponse
            {
                Timestamp = DateTime.Now
            };
        }

        public async Task<CreateAssetResponse> CreateAssetAsync(CreateAssetRequest request)
        {
            await using var db = new DatabaseContext();

            AssetBase asset = request.Asset switch
            {
                CreateStockAssetDto stockDto => (await db.StocksAssets.AddAsync(new StockAsset
                {
                    ExchangeId = stockDto.ExchangeId,
                    Ticker = stockDto.Ticker,
                    Description = stockDto.Description,
                    Company = stockDto.Company
                })).Entity,
                CreateIndexAssetDto indexDto => (await db.IndexAssets.AddAsync(new IndexAsset
                {
                    ExchangeId = indexDto.ExchangeId,
                    Ticker = indexDto.Ticker,
                    Description = indexDto.Description,
                    Country = indexDto.Country
                })).Entity,
                _ => throw new ApiException("Unknown asset type", Status400BadRequest)
            };

            await db.SaveChangesAsync();
            return new CreateAssetResponse()
            {
                Resource = new ResourceDto
                {
                    Id = asset.Id
                }
            };
        }

        public async Task<GetAssetResponse> GetAssetAsync(GetAssetRequest request)
        {
            await using var db = new DatabaseContext();
            var asset = await db.Assets.FindAsync(request.Id);

            return new GetAssetResponse
            {
                Asset = ConvertAssetToDto(asset)
            };
        }

        public async Task<DeleteAssetResponse> DeleteAssetAsync(DeleteAssetRequest request)
        {
            await using var db = new DatabaseContext();
            var asset = await db.Assets.FirstOrDefaultAsync(x => x.Id == request.Id);

            db.Assets.Remove(asset);
            await db.SaveChangesAsync();

            return new DeleteAssetResponse();
        }

        public async Task<GetAssetsByFilterResponse> GetAssetsByFilterAsync(GetAssetsByFilterRequest request)
        {
            await using var db = new DatabaseContext();
            var assetsQuery = db.Assets.AsQueryable();

            if (request.Filter.Type != null)
                assetsQuery = assetsQuery.Where(x => x.Type == request.Filter.Type.ToString());

            if (!string.IsNullOrWhiteSpace(request.Filter.Ticker))
                assetsQuery = assetsQuery.Where(x => x.Ticker.ToLower().Contains(request.Filter.Ticker.ToLower()));

            if (request.Filter.Exchange != null)
            {
                if(request.Filter.Exchange.Id != null)
                    assetsQuery = assetsQuery.Where(x => x.Exchange.Id == request.Filter.Exchange.Id);
                
                if(request.Filter.Exchange.Title != null)
                    assetsQuery = assetsQuery.Where(x => x.Exchange.Title.ToLower().Contains(request.Filter.Exchange.Title.ToLower()));
                
                if(request.Filter.Exchange.EngineType != null)
                    assetsQuery = assetsQuery.Where(x => x.Exchange.EngineType == request.Filter.Exchange.EngineType.ToString());
            }

            var assets = await assetsQuery.Skip(request.Filter.Shift).Take(request.Filter.Count).ToListAsync();
            return new GetAssetsByFilterResponse
            {
                Assets = assets.Select(ConvertAssetToDto).ToList()
            };
        }

        public async Task<CreateExchangeResponse> CreateExchangeAsync(CreateExchangeRequest request)
        {
            await using var db = new DatabaseContext();

            var exchange = await db.Exchanges.AddAsync(new Exchange
            {
                Title = request.Exchange.Title,
                Description = request.Exchange.Description,
                Website = request.Exchange.Website,
                EngineType = request.Exchange.EngineType.ToString()
            });
            await db.SaveChangesAsync();

            return new CreateExchangeResponse
            {
                Resource = new ResourceDto
                {
                    Id = exchange.Entity.Id
                }
            };
        }

        public async Task<GetExchangeResponse> GetExchangeAsync(GetExchangeRequest request)
        {
            await using var db = new DatabaseContext();
            var exchange = await db.Exchanges.FindAsync(request.Id);

            return new GetExchangeResponse
            {
                Exchange = ConvertExchangeToDto(exchange)
            };
        }

        public async Task<DeleteExchangeResponse> DeleteExchangeAsync(DeleteExchangeRequest request)
        {
            await using var db = new DatabaseContext();
            var exchange = await db.Exchanges.FirstOrDefaultAsync(x => x.Id == request.Id);

            db.Exchanges.Remove(exchange);
            await db.SaveChangesAsync();

            return new DeleteExchangeResponse();
        }

        public async Task<GetExchangesByFilterResponse> GetExchangesByFilterAsync(GetExchangesByFilterRequest request)
        {
            await using var db = new DatabaseContext();
            var exchangesQuery = db.Exchanges.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Filter.Title))
                exchangesQuery = exchangesQuery.Where(x => x.Title.ToLower().Contains(request.Filter.Title.ToLower()));
            
            if (request.Filter.EngineType != null)
                exchangesQuery = exchangesQuery.Where(x => x.EngineType == request.Filter.EngineType.ToString());

            var exchanges = await exchangesQuery.Skip(request.Filter.Shift).Take(request.Filter.Count).ToListAsync();
            return new GetExchangesByFilterResponse
            {
                Exchanges = exchanges.Select(ConvertExchangeToDto).ToList()
            };
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Account service started");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Account service stopped");
        }

        private ExchangeDto ConvertExchangeToDto(Exchange exchange)
        {
            return new ExchangeDto
            {
                Id = exchange.Id,
                Title = exchange.Title,
                Description = exchange.Description,
                Website = exchange.Website,
                EngineType = Enum.Parse<EngineTypeDto>(exchange.EngineType)
            };
        }

        private StockAssetDto ConvertStockToDto(StockAsset stock)
        {
            return new StockAssetDto
            {
                Id = stock.Id,
                ExchangeId = stock.ExchangeId,
                Ticker = stock.Ticker,
                Description = stock.Description,
                Company = stock.Company
            };
        }

        private IndexAssetDto ConvertIndexToDto(IndexAsset index)
        {
            return new IndexAssetDto
            {
                Id = index.Id,
                ExchangeId = index.ExchangeId,
                Ticker = index.Ticker,
                Description = index.Description,
                Country = index.Country
            };
        }

        private AssetBaseDto ConvertAssetToDto(AssetBase asset)
        {
            return asset switch
            {
                StockAsset stock => ConvertStockToDto(stock),
                IndexAsset index => ConvertIndexToDto(index),
                _ => null
            };
        }
    }
}