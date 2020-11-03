using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Database;
using OneGate.Backend.Database.Models;
using OneGate.Shared.Models.Asset;

namespace OneGate.Backend.Services.AssetService.Repository
{
    public class AssetRepository : IAssetRepository
    {
        private readonly DatabaseContext _db;

        public AssetRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<int> AddAsync(CreateAssetBaseDto model)
        {
            AssetBase assetBase = model switch
            {
                CreateStockAssetDto stockDto => new StockAsset
                {
                    ExchangeId = stockDto.ExchangeId,
                    Ticker = stockDto.Ticker,
                    Description = stockDto.Description,
                    Company = stockDto.Company
                },
                CreateIndexAssetDto indexDto => new IndexAsset
                {
                    ExchangeId = indexDto.ExchangeId,
                    Ticker = indexDto.Ticker,
                    Description = indexDto.Description,
                    Country = indexDto.Country
                },
                _ => throw new ArgumentException("Invalid asset type")
            };

            var asset = await _db.Assets.AddAsync(assetBase);
            await _db.SaveChangesAsync();

            return asset.Entity.Id;
        }

        public async Task<AssetBaseDto> FindAsync(int id)
        {
            var asset = await _db.Assets.FirstOrDefaultAsync(x => x.Id == id);
            return ConvertAssetToDto(asset);
        }

        public async Task<IEnumerable<AssetBaseDto>> FilterAsync(AssetBaseFilterDto model)
        {
            var assetsQuery = _db.Assets.AsQueryable();

            if (model.Type != null)
                assetsQuery = assetsQuery.Where(x => x.Type == model.Type.ToString());

            if (!string.IsNullOrWhiteSpace(model.Ticker))
                assetsQuery = assetsQuery.Where(x => x.Ticker.ToLower().Contains(model.Ticker.ToLower()));

            if (model.Exchange != null)
            {
                if (model.Exchange.Id != null)
                    assetsQuery = assetsQuery.Where(x => x.Exchange.Id == model.Exchange.Id);

                if (model.Exchange.Title != null)
                    assetsQuery = assetsQuery.Where(x =>
                        x.Exchange.Title.ToLower().Contains(model.Exchange.Title.ToLower()));

                if (model.Exchange.EngineType != null)
                    assetsQuery = assetsQuery.Where(x =>
                        x.Exchange.EngineType == model.Exchange.EngineType.ToString());
            }

            var assets = await assetsQuery.Skip(model.Shift).Take(model.Count).ToListAsync();
            return assets.Select(ConvertAssetToDto);
        }

        public async Task RemoveAsync(int id)
        {
            _db.Assets.RemoveRange(_db.Assets.Where(x => x.Id == id));
            await _db.SaveChangesAsync();
        }

        private static AssetBaseDto ConvertAssetToDto(AssetBase asset)
        {
            if (asset is null)
                return null;
            
            return asset switch
            {
                StockAsset stock => ConvertStockToDto(stock),
                IndexAsset index => ConvertIndexToDto(index),
                _ => null
            };
        }

        private static StockAssetDto ConvertStockToDto(StockAsset stock)
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

        private static IndexAssetDto ConvertIndexToDto(IndexAsset index)
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
    }
}