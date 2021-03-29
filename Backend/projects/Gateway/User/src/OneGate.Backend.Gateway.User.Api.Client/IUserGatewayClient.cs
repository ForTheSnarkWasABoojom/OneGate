using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Backend.Gateway.User.Api.Contracts.Account;
using OneGate.Backend.Gateway.User.Api.Contracts.Asset;
using OneGate.Backend.Gateway.User.Api.Contracts.Credentials;
using OneGate.Backend.Gateway.User.Api.Contracts.Exchange;
using OneGate.Backend.Gateway.User.Api.Contracts.Order;
using OneGate.Backend.Gateway.User.Api.Contracts.Portfolio;

namespace OneGate.Backend.Gateway.User.Api.Client
{
    public interface IUserGatewayClient
    {
        public Task CreateAccountAsync(CreateAccountRequest model);

        public Task<AccountModel> GetCurrentAccountAsync();
        
        public Task<IEnumerable<AssetModel>> GetAssetsRangeAsync(FilterAssetsRequest request);


        public Task<AssetModel> GetAssetAsync(int id);


        public Task CreateTokenAsync(AuthRequest request);

        public Task<IEnumerable<ExchangeModel>> GetExchangesRangeAsync(FilterExchangesRequest request);


        public Task<ExchangeModel> GetExchangeAsync(int id);

        public Task CreateOrderAsync(CreateOrderRequest request);


        public Task<IEnumerable<OrderModel>> GetOrdersRangeAsync(FilterOrdersRequest request);


        public Task<OrderModel> GetOrderAsync(int id);


        public Task DeleteOrderAsync(int id);


        public Task CreatePortfolioAsync(CreatePortfolioRequest request);


        public Task<IEnumerable<PortfolioModel>> GetPortfoliosRangeAsync(FilterPortfoliosRequest request);


        public Task<PortfolioModel> GetPortfolioAsync(int id);


        public Task DeletePortfolioAsync(int id);
    }
}