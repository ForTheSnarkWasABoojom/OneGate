using System.Threading.Tasks;
using OneGate.Backend.Contracts.Account;
using OneGate.Backend.Contracts.Common;
using OneGate.Backend.Contracts.Order;

namespace OneGate.Backend.Rpc.Services
{
    public interface IAccountService
    {
        public Task<CreatedResourceResponse> CreateAccount(CreateAccount request);
        public Task<AccountsResponse> GetAccountsRange(GetAccounts request);
        public Task<SuccessResponse> DeleteAccount(DeleteAccount request);
        
        public Task<CreatedResourceResponse> CreateOrder(CreateOrder request);
        public Task<OrdersResponse> GetOrdersRange(GetOrders request);
        public Task<SuccessResponse> DeleteOrder(DeleteOrder request);
    }
}