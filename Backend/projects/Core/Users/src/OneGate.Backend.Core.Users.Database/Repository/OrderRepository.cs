using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Core.Users.Database.Models;

namespace OneGate.Backend.Core.Users.Database.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DatabaseContext _db;

        public OrderRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<Order> AddAsync(Order model)
        {
            var order = await _db.Orders.AddAsync(model);
            await _db.SaveChangesAsync();
            return order.Entity;
        }

        public async Task<IEnumerable<Order>> FilterAsync(int? id, string type, int? assetId,
            string state, string side, int ownerId, int shift, int count)
        {
            var orderQuery = _db.Orders.Where(x => x.OwnerId == ownerId);

            if (id != null)
                orderQuery = orderQuery.Where(x => x.Id == id);

            if (assetId != null)
                orderQuery = orderQuery.Where(x => x.AssetId == assetId);

            if (type != null)
                orderQuery = orderQuery.Where(x => x.Type == type);

            if (state != null)
                orderQuery = orderQuery.Where(x => x.State == state);

            if (side != null)
                orderQuery = orderQuery.Where(x => x.Side == side);

            return  await orderQuery.Skip(shift).Take(count).ToListAsync();
        }

        public async Task RemoveAsync(int id, int ownerId)
        {
            _db.Orders.RemoveRange(_db.Orders.Where(x =>
                x.Id == id && x.OwnerId == ownerId));
            await _db.SaveChangesAsync();
        }
    }
}