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
using OneGate.Shared.Models.Exchange;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace OneGate.Backend.AssetService
{
    public class AssetService : IHostedService, IAssetService
    {
        private readonly ILogger<AssetService> _logger;
        private readonly IBus _bus;

        public AssetService(ILogger<AssetService> logger, IBus bus)
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

            if (await db.Assets.AnyAsync(x => x.Type == request.Asset.Type.ToString() &&
                                              x.ExchangeId == request.Asset.ExchangeId &&
                                              x.Ticker == request.Asset.Ticker))
                throw new ApiException("Asset must have unique type-exchange-ticker triplet", Status400BadRequest);

            if (!await db.Exchanges.AnyAsync(x => x.Id == request.Asset.ExchangeId))
                throw new ApiException("Exchange with specified id does not exist", Status400BadRequest);

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
                Asset = ConvertAssetToDto(asset)
            };
        }

        public async Task<GetAssetResponse> GetAssetAsync(GetAssetRequest request)
        {
            await using var db = new DatabaseContext();
            var asset = await db.Assets.FindAsync(request.Id);

            if (asset is null)
                throw new ApiException("Asset with specified id does not exist", Status404NotFound);

            return new GetAssetResponse
            {
                Asset = ConvertAssetToDto(asset)
            };
        }

        public async Task<DeleteAssetResponse> DeleteAssetAsync(DeleteAssetRequest request)
        {
            await using var db = new DatabaseContext();
            var asset = await db.Assets.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (asset == null)
                throw new ApiException("Account with specified id does not exist", Status404NotFound);

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
                assetsQuery = assetsQuery.Where(x => x.Ticker.Contains(request.Filter.Ticker, StringComparison.OrdinalIgnoreCase));

            if (request.Filter.ExchangeId != null)
                assetsQuery = assetsQuery.Where(x => x.ExchangeId == request.Filter.ExchangeId);

            var assets = await assetsQuery.Skip(request.Filter.Shift).Take(request.Filter.Count).ToListAsync();
            return new GetAssetsByFilterResponse
            {
                Assets = assets.Select(ConvertAssetToDto).ToList()
            };
        }

        public async Task<CreateExchangeResponse> CreateExchangeAsync(CreateExchangeRequest request)
        {
            await using var db = new DatabaseContext();
            if (await db.Exchanges.AnyAsync(x => x.Title == request.Exchange.Title))
                throw new ApiException("Exchange must have unique title", Status400BadRequest);

            var exchange = await db.Exchanges.AddAsync(new Exchange
            {
                Title = request.Exchange.Title,
                Description = request.Exchange.Description,
                Website = request.Exchange.Website
            });
            await db.SaveChangesAsync();

            return new CreateExchangeResponse()
            {
                Exchange = ConvertExchangeToDto(exchange.Entity)
            };
        }

        public async Task<GetExchangeResponse> GetExchangeAsync(GetExchangeRequest request)
        {
            await using var db = new DatabaseContext();
            var exchange = await db.Exchanges.FindAsync(request.Id);

            if (exchange is null)
                throw new ApiException("Exchange with specified id does not exist", Status404NotFound);

            return new GetExchangeResponse
            {
                Exchange = ConvertExchangeToDto(exchange)
            };
        }

        public async Task<DeleteExchangeResponse> DeleteExchangeAsync(DeleteExchangeRequest request)
        {
            await using var db = new DatabaseContext();
            var exchange = await db.Exchanges.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (exchange == null)
                throw new ApiException("Exchange with specified id does not exist", Status404NotFound);

            db.Exchanges.Remove(exchange);
            await db.SaveChangesAsync();

            return new DeleteExchangeResponse();
        }

        public async Task<GetExchangesByFilterResponse> GetExchangesByFilterAsync(GetExchangesByFilterRequest request)
        {
            await using var db = new DatabaseContext();
            var exchangesQuery = db.Exchanges.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Filter.Title))
                exchangesQuery = exchangesQuery.Where(x => x.Title.Contains(request.Filter.Title, StringComparison.OrdinalIgnoreCase));

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
                Website = exchange.Website
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