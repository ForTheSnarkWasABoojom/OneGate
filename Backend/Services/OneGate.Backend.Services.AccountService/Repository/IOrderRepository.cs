using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Shared.Models.Order;

namespace OneGate.Backend.Services.AccountService.Repository
{
    public interface IOrderRepository
    {
        public Task<int> AddAsync(CreateOrderBaseDto model, int ownerId);
        public Task<OrderBaseDto> FindAsync(int id, int ownerId);
        public Task<IEnumerable<OrderBaseDto>> FilterAsync(OrderBaseFilterDto filter, int ownerId);
        public Task RemoveAsync(int id, int ownerId);
    }
}