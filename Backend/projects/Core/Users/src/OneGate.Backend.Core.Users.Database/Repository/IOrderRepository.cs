using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Backend.Core.Users.Database.Models;

namespace OneGate.Backend.Core.Users.Database.Repository
{
    public interface IOrderRepository
    {
        public Task<int> AddAsync(Order orderBase);

        public Task<IEnumerable<Order>> FilterAsync(int? id, string type, int? assetId,
            string state, string side, int ownerId, int shift, int count);

        public Task RemoveAsync(int id, int ownerId);
    }
}