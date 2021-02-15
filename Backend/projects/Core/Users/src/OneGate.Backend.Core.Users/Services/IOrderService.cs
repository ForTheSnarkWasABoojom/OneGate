using System.Threading.Tasks;
using OneGate.Backend.Core.Users.Contracts.Order;
using OneGate.Backend.Transport.Bus.Contracts;
using OneGate.Backend.Transport.Contracts;

namespace OneGate.Backend.Core.Users.Services
{
    public interface IOrderService
    {
        public Task<CreatedResourceResponse> CreateOrderAsync(CreateOrder request);
        public Task<OrdersResponse> GetOrdersAsync(GetOrders request);
        public Task<SuccessResponse> DeleteOrderAsync(DeleteOrder request);
    }
}