using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Core.Records.Database.Models;

namespace OneGate.Backend.Core.Records.Database.Repository
{
    public class AssetRepository : IAssetRepository
    {
        private readonly DatabaseContext _db;

        public AssetRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<Asset> AddAsync(Asset model)
        {
            var asset = await _db.Assets.AddAsync(model);
            await _db.SaveChangesAsync();
            return asset.Entity;
        }

        public async Task<IEnumerable<Asset>> FilterAsync(int? id, string type, string ticker, int? exchangeId,
            string exchangeTitle, string exchangeEngineType, int shift, int count)
        {
            var assetsQuery = _db.Assets.AsQueryable();

            if (id != null)
                assetsQuery = assetsQuery.Where(x => x.Id == id);

            if (type != null)
                assetsQuery = assetsQuery.Where(x => x.Type == type);

            if (!string.IsNullOrWhiteSpace(ticker))
                assetsQuery = assetsQuery.Where(x => x.Ticker.ToLower().Contains(ticker.ToLower()));

            if (exchangeId != null)
                assetsQuery = assetsQuery.Where(x => x.Exchange.Id == exchangeId);

            if (exchangeTitle != null)
                assetsQuery = assetsQuery.Where(x =>
                    x.Exchange.Title.ToLower().Contains(exchangeTitle.ToLower()));

            if (exchangeEngineType != null)
                assetsQuery = assetsQuery.Where(x =>
                    x.Exchange.EngineType == exchangeEngineType);


            return await assetsQuery.Skip(shift).Take(count).ToListAsync();
        }

        public async Task RemoveAsync(int id)
        {
            _db.Assets.RemoveRange(_db.Assets.Where(x => x.Id == id));
            await _db.SaveChangesAsync();
        }
    }
}