using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using OneGate.Backend.Gateway.Shared.Api.Client;
using OneGate.Backend.Gateway.User.Api.Contracts.Account;
using OneGate.Backend.Gateway.User.Api.Contracts.Asset;
using OneGate.Backend.Gateway.User.Api.Contracts.Credentials;
using OneGate.Backend.Gateway.User.Api.Contracts.Exchange;
using OneGate.Backend.Gateway.User.Api.Contracts.Order;
using OneGate.Backend.Gateway.User.Api.Contracts.Portfolio;

namespace OneGate.Backend.Gateway.User.Api.Client
{
    public class UserGatewayClient : IUserGatewayClient
    {
        private readonly Uri _baseUrl;

        public UserGatewayClient(IOptions<UserGatewayClientOptions> options)
        {
            _baseUrl = options.Value.BaseUri;
        }

        public async Task CreateAccountAsync(CreateAccountRequest model)
        {
            var result = await _baseUrl
                .AppendPathSegment("accounts")
                .PostJsonAsync(model);
        }

        public async Task<AccountModel> GetCurrentAccountAsync()
        {
            var result = await _baseUrl
                .AppendPathSegment("accounts")
                .GetJsonAsync<AccountModel>();

            return result;
        }

        public async Task<IEnumerable<AssetModel>> GetAssetsRangeAsync(FilterAssetsRequest request)
        {
            var result = await _baseUrl
                .AppendPathSegment("assets")
                .SetQueryParamsFromModel(request)
                .GetJsonAsync<IEnumerable<AssetModel>>();

            return result; 
        }
        
        public async Task<AssetModel> GetAssetAsync(int id)
        {
            var result = await _baseUrl
                .AppendPathSegment("assets")
                .AppendPathSegment(id)
                .GetJsonAsync<AssetModel>();

            return result; 
        }

        public async Task<TokenResponse> CreateTokenAsync(AuthRequest request)
        {
            var result = await _baseUrl
                .AppendPathSegment("credentials/auth")
                .PostJsonAsync(request)
                .ReceiveJson<TokenResponse>();
            
            return result;
        }
        
        
        public async Task<IEnumerable<ExchangeModel>> GetExchangesRangeAsync(FilterExchangesRequest request)
        {
            var result = await _baseUrl
                .AppendPathSegment("exchanges")
                .SetQueryParamsFromModel(request)
                .GetJsonAsync<IEnumerable<ExchangeModel>>();

            return result;
        }

        public async Task<ExchangeModel> GetExchangeAsync(int id)
        {
            var result = await _baseUrl
                .AppendPathSegment("exchanges")
                .AppendPathSegment(id)
                .GetJsonAsync<ExchangeModel>();

            return result;
        }
        public async Task CreateOrderAsync(CreateOrderRequest request)
        {
            var result = await _baseUrl
                .AppendPathSegment("orders")
                .PostJsonAsync(request);
        }
        public async Task<OrderModel> GetOrderAsync(int id)
        {
            var result = await _baseUrl
                .AppendPathSegment("orders")
                .AppendPathSegment(id)
                .GetJsonAsync<OrderModel>();

            return result;
        }
        public async Task<IEnumerable<OrderModel>> GetOrdersRangeAsync(FilterOrdersRequest request)
        {
            var result = await _baseUrl
                .AppendPathSegment("orders")
                .SetQueryParamsFromModel(request)
                .GetJsonAsync<IEnumerable<OrderModel>>();

            return result;
        }

        public async Task DeleteOrderAsync(int id)
        {
            await _baseUrl
                .AppendPathSegment("orders")
                .AppendPathSegment(id)
                .DeleteAsync();
        }

        public async Task CreatePortfolioAsync(CreatePortfolioRequest request)
        {
            var result = await _baseUrl
                .AppendPathSegment("portfolios")
                .PostJsonAsync(request);

        }

        public async Task<PortfolioModel> GetPortfolioAsync(int id)
        {
            var result = await _baseUrl
                .AppendPathSegment("portfolios")
                .AppendPathSegment(id)
                .GetJsonAsync<PortfolioModel>();

            return result;
        }
        
        public async Task<IEnumerable<PortfolioModel>> GetPortfoliosRangeAsync(FilterPortfoliosRequest request)
        {
            var result = await _baseUrl
                .AppendPathSegment("portfolios")
                .SetQueryParamsFromModel(request)
                .GetJsonAsync<IEnumerable<PortfolioModel>>();

            return result;
        }

        public async Task DeletePortfolioAsync(int id)
        {
            await _baseUrl
                .AppendPathSegment("portfolios")
                .AppendPathSegment(id)
                .DeleteAsync();
        }
    }
}