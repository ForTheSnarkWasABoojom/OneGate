using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Core.Users.Database.Models;

namespace OneGate.Backend.Core.Users.Database.Repository
{
    public class PorfolioAssetLinkRepository : IPorfolioAssetLinkRepository
    {
        private readonly DatabaseContext _db;

        public PorfolioAssetLinkRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<int> AddAsync(PortfolioAssetLink model)
        {
            var link = await _db.PortfolioAssetLinks.AddAsync(model);

            await _db.SaveChangesAsync();

            return link.Entity.Id;
        }

        public async Task<IEnumerable<PortfolioAssetLink>> FilterAsync(int? id, int? portfolioId, int? assetId,
            int shift, int count)
        {
            var linkQuery = _db.PortfolioAssetLinks.AsQueryable();

            if (id != null)
                linkQuery = linkQuery.Where(x => x.Id == id);

            if (assetId != null)
                linkQuery = linkQuery.Where(x => x.AssetId == assetId);

            if (portfolioId != null)
                linkQuery = linkQuery.Where(x => x.PortfolioId == portfolioId);

            return await linkQuery.Skip(shift).Take(count).ToListAsync();
        }

        public async Task RemoveAsync(int id)
        {
            _db.PortfolioAssetLinks.RemoveRange(_db.PortfolioAssetLinks.Where(x =>
                x.Id == id));
            await _db.SaveChangesAsync();
        }
    }
}