using System.Threading.Tasks;
using OneGate.Backend.Transport.Contracts.Account;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Order;
using OneGate.Backend.Transport.Contracts.Portfolio;
using OneGate.Backend.Transport.Contracts.PortfolioAssetLink;

namespace OneGate.Backend.Core.User.Service
{
    public interface IUserService
    {
        public Task<CreatedResourceResponse> CreateAccount(CreateAccount request);
        public Task<AccountsResponse> GetAccounts(GetAccounts request);
        public Task<SuccessResponse> DeleteAccount(DeleteAccount request);
        public Task<AuthorizationResponse> CreateAuthorizationContext(CreateAuthorizationContext request);
        public Task<CreatedResourceResponse> CreateOrder(CreateOrder request);
        public Task<OrdersResponse> GetOrders(GetOrders request);
        public Task<SuccessResponse> DeleteOrder(DeleteOrder request);
        
        public Task<CreatedResourceResponse> CreatePortfolio(CreatePortfolio request); 
        public Task<PortfoliosResponse> GetPortfolios(GetPortfolios request); 
        public Task<SuccessResponse> DeletePortfolio(DeletePortfolio request);
        
        public Task<CreatedResourceResponse> CreatePortfolioAssetLink(CreatePortfolioAssetLink request); 
        public Task<PortfolioAssetLinksResponse> GetPortfolioAssetLinks(GetPortfolioAssetLinks request); 
        public Task<SuccessResponse> DeletePortfolioAssetLink(DeletePortfolioAssetLink request);
    }
}