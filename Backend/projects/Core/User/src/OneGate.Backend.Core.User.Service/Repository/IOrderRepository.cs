using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Common.Models.Order;

namespace OneGate.Backend.Core.User.Service.Repository
{
    public interface IOrderRepository
    {
        public Task<int> AddAsync(CreateOrderDto model, int ownerId);
        public Task<OrderDto> FindAsync(int id, int ownerId);
        public Task<IEnumerable<OrderDto>> FilterAsync(OrderFilterDto filter, int ownerId);
        public Task RemoveAsync(int id, int ownerId);
    }
}