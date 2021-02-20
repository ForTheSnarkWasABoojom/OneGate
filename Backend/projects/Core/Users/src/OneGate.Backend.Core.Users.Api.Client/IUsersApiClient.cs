using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Backend.Core.Users.Api.Contracts.Account;
using OneGate.Backend.Core.Users.Api.Contracts.Order;
using OneGate.Backend.Core.Users.Api.Contracts.Portfolio;

namespace OneGate.Backend.Core.Users.Api.Client
{
    public interface IUsersApiClient
    {
        public Task<AccountDto> CreateAccountAsync(CreateAccountDto request);
        public Task<IEnumerable<AccountDto>> GetAccountsAsync(FilterAccountsDto request);
        public Task DeleteAccountAsync(int id);
        public Task<int> CreateOrderAsync(OrderDto request);
        public Task<IEnumerable<OrderDto>> GetOrdersAsync(FilterOrdersDto request);
        public Task DeleteOrderAsync(int id, int ownerId);
        public Task<int> CreatePortfolioAsync(PortfolioDto request);
        public Task<IEnumerable<PortfolioDto>> GetPortfoliosAsync(FilterPortfoliosDto request);
        public Task DeletePortfolioAsync(int id, int ownerId);
    }
}