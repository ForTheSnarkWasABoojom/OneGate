using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Shared.Models.Order;

namespace OneGate.Backend.Services.AccountService.Repository
{
    public interface IOrderRepository
    {
        public Task<int> AddAsync(CreateOrderDto model, int ownerId);
        public Task<OrderDto> FindAsync(int id, int ownerId);
        public Task<IEnumerable<OrderDto>> FilterAsync(OrderFilterDto filter, int ownerId);
        public Task RemoveAsync(int id, int ownerId);
    }
}