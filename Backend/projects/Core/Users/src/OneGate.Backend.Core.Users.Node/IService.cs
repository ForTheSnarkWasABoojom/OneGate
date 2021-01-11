using System.Threading.Tasks;
using OneGate.Backend.Transport.Contracts.Account;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Order;
using OneGate.Backend.Transport.Contracts.Portfolio;
using OneGate.Backend.Transport.Contracts.PortfolioAssetLink;

namespace OneGate.Backend.Core.Users.Node
{
    public interface IService
    {
        public Task<CreatedResourceResponse> CreateAccountAsync(CreateAccount request);
        public Task<AccountsResponse> GetAccountsAsync(GetAccounts request);
        public Task<SuccessResponse> DeleteAccountAsync(DeleteAccount request);
        public Task<AuthorizationResponse> CreateAuthorizationContext(CreateAuthorizationContext request);
        
        public Task<CreatedResourceResponse> CreateOrderAsync(CreateOrder request);
        public Task<OrdersResponse> GetOrdersAsync(GetOrders request);
        public Task<SuccessResponse> DeleteOrderAsync(DeleteOrder request);
        
        public Task<CreatedResourceResponse> CreatePortfolioAsync(CreatePortfolio request); 
        public Task<PortfoliosResponse> GetPortfoliosAsync(GetPortfolios request); 
        public Task<SuccessResponse> DeletePortfolioAsync(DeletePortfolio request);
    }
}