using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Core.Records.Database.Models;

namespace OneGate.Backend.Core.Records.Database.Repository
{
    public class ExchangeRepository : IExchangeRepository
    {
        private readonly DatabaseContext _db;

        public ExchangeRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<Exchange> AddAsync(Exchange model)
        {
            var exchange = await _db.Exchanges.AddAsync(model);
            await _db.SaveChangesAsync();
            return exchange.Entity;
        }

        public async Task<IEnumerable<Exchange>> FilterAsync(int? id, string title, string engineType, int shift,
            int count)
        {
            var exchangesQuery = _db.Exchanges.AsQueryable();

            if (id != null)
                exchangesQuery = exchangesQuery.Where(x => x.Id == id);

            if (!string.IsNullOrWhiteSpace(title))
                exchangesQuery = exchangesQuery.Where(x => x.Title.ToLower().Contains(title.ToLower()));

            if (engineType != null)
                exchangesQuery = exchangesQuery.Where(x => x.EngineType == engineType);
            
            return await exchangesQuery.Skip(shift).Take(count).ToListAsync();
        }

        public async Task RemoveAsync(int id)
        {
            _db.Exchanges.RemoveRange(_db.Exchanges.Where(x => x.Id == id));
            await _db.SaveChangesAsync();
        }
    }
}