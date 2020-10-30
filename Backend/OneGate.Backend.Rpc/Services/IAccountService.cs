using System.Threading.Tasks;
using OneGate.Backend.Contracts.Account;
using OneGate.Backend.Contracts.Common;
using OneGate.Backend.Contracts.Order;

namespace OneGate.Backend.Rpc.Services
{
    public interface IAccountService
    {
        public Task<CreatedResourceResponse> CreateAccount(CreateAccount request);
        public Task<AccountResponse> GetAccount(GetAccount request);
        public Task<AccountsRangeResponse> GetAccountsRange(GetAccountsRange request);
        public Task<SuccessResponse> DeleteAccount(DeleteAccount request);
        
        public Task<CreatedResourceResponse> CreateOrder(CreateOrder request);
        public Task<OrderResponse> GetOrder(GetOrder request);
        public Task<OrdersRangeResponse> GetOrdersRange(GetOrdersRange request);
        public Task<SuccessResponse> DeleteOrder(DeleteOrder request);
    }
}