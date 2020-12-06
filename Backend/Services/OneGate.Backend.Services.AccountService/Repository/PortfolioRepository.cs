using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Database;
using OneGate.Backend.Database.Models;
using OneGate.Shared.Models.Portfolio;

namespace OneGate.Backend.Services.AccountService.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly DatabaseContext _db;

        public PortfolioRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<int> AddAsync(CreatePortfolioDto model)
        {
            var portfolio = await _db.Portfolios.AddAsync(new Portfolio
            {
                Name = model.Name,
                Description = model.Description,
                OwnerId = model.OwnerId
            });

            await _db.SaveChangesAsync();

            return portfolio.Entity.Id;
        }

        public async Task<IEnumerable<PortfolioDto>> FilterAsync(PortfolioFilterDto filter, int ownerId)
        {
            var portfolioQuery = _db.Portfolios.Where(x => x.OwnerId == ownerId);

            if (filter.Id != null)
                portfolioQuery = portfolioQuery.Where(x => x.Id == filter.Id);

            if (filter.Name != null)
                portfolioQuery = portfolioQuery.Where(x => x.Name == filter.Name);

            var orders = await portfolioQuery.Skip(filter.Shift).Take(filter.Count).ToListAsync();
            return orders.Select(ConvertPortfolioToDto);
        }

        public async Task RemoveAsync(int id, int ownerId)
        {
            _db.Portfolios.RemoveRange(_db.Portfolios.Where(x =>
                x.Id == id && x.OwnerId == ownerId));
            await _db.SaveChangesAsync();
        }

        private static PortfolioDto ConvertPortfolioToDto(Portfolio portfolio)
        {
            return new PortfolioDto
            {
                Id = portfolio.Id,
                Name = portfolio.Name,
                Description = portfolio.Description,
                OwnerId = portfolio.OwnerId
            };
        }
    }
}