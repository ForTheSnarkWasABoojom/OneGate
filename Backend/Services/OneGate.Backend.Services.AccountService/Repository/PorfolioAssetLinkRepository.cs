using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Database;
using OneGate.Backend.Database.Models;
using OneGate.Shared.Models.PortfolioAssetLink;

namespace OneGate.Backend.Services.AccountService.Repository
{
    public class PorfolioAssetLinkRepository : IPorfolioAssetLinkRepository
    {
        private readonly DatabaseContext _db;

        public PorfolioAssetLinkRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<int> AddAsync(CreatePortfolioAssetLinkDto model)
        {
            var link = await _db.PortfolioAssetLinks.AddAsync(new PortfolioAssetLink
            {
                Count = model.Count,
                PortfolioId = model.PortfolioId,
                AssetId = model.AssetId
            });

            await _db.SaveChangesAsync();

            return link.Entity.Id;
        }

        public async Task<IEnumerable<PortfolioAssetLinkDto>> FilterAsync(PortfolioAssetLinkFilterDto filter)
        {
            var linkQuery = _db.PortfolioAssetLinks.AsQueryable();
            ;

            if (filter.Id != null)
                linkQuery = linkQuery.Where(x => x.Id == filter.Id);

            if (filter.AssetId != null)
                linkQuery = linkQuery.Where(x => x.AssetId == filter.AssetId);

            if (filter.PortfolioId != null)
                linkQuery = linkQuery.Where(x => x.PortfolioId == filter.PortfolioId);

            var links = await linkQuery.Skip(filter.Shift).Take(filter.Count).ToListAsync();
            return links.Select(ConvertLinkToDto);
        }

        public async Task RemoveAsync(int id)
        {
            _db.PortfolioAssetLinks.RemoveRange(_db.PortfolioAssetLinks.Where(x =>
                x.Id == id));
            await _db.SaveChangesAsync();
        }

        private static PortfolioAssetLinkDto ConvertLinkToDto(PortfolioAssetLink link)
        {
            return new PortfolioAssetLinkDto
            {
                Id = link.Id,
                AssetId = link.AssetId,
                PortfolioId = link.PortfolioId,
                Count = link.Count
            };
        }
    }
}