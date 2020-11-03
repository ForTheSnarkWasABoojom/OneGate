using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Database;
using OneGate.Backend.Database.Models;
using OneGate.Shared.Models.Exchange;

namespace OneGate.Backend.Services.AssetService.Repository
{
    public class ExchangeRepository : IExchangeRepository
    {
        private readonly DatabaseContext _db;

        public ExchangeRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<int> AddAsync(CreateExchangeDto model)
        {
            var exchange = await _db.Exchanges.AddAsync(new Exchange
            {
                Title = model.Title,
                Description = model.Description,
                Website = model.Website,
                EngineType = model.EngineType.ToString()
            });
            await _db.SaveChangesAsync();

            return exchange.Entity.Id;
        }

        public async Task<ExchangeDto> FindAsync(int id)
        {
            var exchange = await _db.Exchanges.FirstOrDefaultAsync(x => x.Id == id);
            return ConvertExchangeToDto(exchange);
        }

        public async Task<IEnumerable<ExchangeDto>> FilterAsync(ExchangeFilterDto filter)
        {
            var exchangesQuery = _db.Exchanges.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Title))
                exchangesQuery = exchangesQuery.Where(x => x.Title.ToLower().Contains(filter.Title.ToLower()));

            if (filter.EngineType != null)
                exchangesQuery = exchangesQuery.Where(x => x.EngineType == filter.EngineType.ToString());

            var exchanges = await exchangesQuery.Skip(filter.Shift).Take(filter.Count).ToListAsync();

            return exchanges.Select(ConvertExchangeToDto);
        }

        public async Task RemoveAsync(int id)
        {
            _db.Exchanges.RemoveRange(_db.Exchanges.Where(x => x.Id == id));
            await _db.SaveChangesAsync();
        }

        private static ExchangeDto ConvertExchangeToDto(Exchange exchange)
        {
            if (exchange is null)
                return null;
            
            return new ExchangeDto
            {
                Id = exchange.Id,
                Title = exchange.Title,
                Description = exchange.Description,
                Website = exchange.Website,
                EngineType = Enum.Parse<EngineTypeDto>(exchange.EngineType)
            };
        }
    }
}