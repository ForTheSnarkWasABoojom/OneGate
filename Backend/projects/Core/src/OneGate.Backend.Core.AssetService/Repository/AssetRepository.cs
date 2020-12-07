using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Database;
using OneGate.Backend.Database.Models;
using OneGate.Common.Models.Asset;

namespace OneGate.Backend.Core.AssetService.Repository
{
    public class AssetRepository : IAssetRepository
    {
        private readonly DatabaseContext _db;

        public AssetRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<int> AddAsync(CreateAssetDto model)
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

        public async Task<AssetDto> FindAsync(int id)
        {
            var asset = await _db.Assets.FirstOrDefaultAsync(x => x.Id == id);
            return ConvertAssetToDto(asset);
        }

        public async Task<IEnumerable<AssetDto>> FilterAsync(AssetFilterDto filter)
        {
            var assetsQuery = _db.Assets.AsQueryable();

            if (filter.Id != null)
                assetsQuery = assetsQuery.Where(x => x.Id == filter.Id);
            
            if (filter.Type != null)
                assetsQuery = assetsQuery.Where(x => x.Type == filter.Type.ToString());

            if (!string.IsNullOrWhiteSpace(filter.Ticker))
                assetsQuery = assetsQuery.Where(x => x.Ticker.ToLower().Contains(filter.Ticker.ToLower()));

            if (filter.Exchange != null)
            {
                if (filter.Exchange.Id != null)
                    assetsQuery = assetsQuery.Where(x => x.Exchange.Id == filter.Exchange.Id);

                if (filter.Exchange.Title != null)
                    assetsQuery = assetsQuery.Where(x =>
                        x.Exchange.Title.ToLower().Contains(filter.Exchange.Title.ToLower()));

                if (filter.Exchange.EngineType != null)
                    assetsQuery = assetsQuery.Where(x =>
                        x.Exchange.EngineType == filter.Exchange.EngineType.ToString());
            }

            var assets = await assetsQuery.Skip(filter.Shift).Take(filter.Count).ToListAsync();
            return assets.Select(ConvertAssetToDto);
        }

        public async Task RemoveAsync(int id)
        {
            _db.Assets.RemoveRange(_db.Assets.Where(x => x.Id == id));
            await _db.SaveChangesAsync();
        }

        private static AssetDto ConvertAssetToDto(AssetBase asset)
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