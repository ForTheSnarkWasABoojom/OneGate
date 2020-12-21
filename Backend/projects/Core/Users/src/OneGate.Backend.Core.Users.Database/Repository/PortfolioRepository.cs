using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Core.Users.Database.Models;

namespace OneGate.Backend.Core.Users.Database.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly DatabaseContext _db;

        public PortfolioRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<int> AddAsync(Portfolio model)
        {
            var portfolio = await _db.Portfolios.AddAsync(model);

            await _db.SaveChangesAsync();

            return portfolio.Entity.Id;
        }

        public async Task<IEnumerable<Portfolio>> FilterAsync(int? id, int? ownerId, string name,
            int shift, int count)
        {
            var portfolioQuery = _db.Portfolios.Where(x => x.OwnerId == ownerId);

            if (id != null)
                portfolioQuery = portfolioQuery.Where(x => x.Id == id);

            if (name != null)
                portfolioQuery = portfolioQuery.Where(x => x.Name == name);

            return await portfolioQuery.Skip(shift).Take(count).ToListAsync();
        }

        public async Task RemoveAsync(int id, int ownerId)
        {
            _db.Portfolios.RemoveRange(_db.Portfolios.Where(x =>
                x.Id == id && x.OwnerId == ownerId));
            await _db.SaveChangesAsync();
        }
    }
}