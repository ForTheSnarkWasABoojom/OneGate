using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using OneGate.Backend.Core.Shared.Api.Client;
using OneGate.Backend.Core.Users.Api.Contracts.Account;
using OneGate.Backend.Core.Users.Api.Contracts.Administrator;
using OneGate.Backend.Core.Users.Api.Contracts.Order;
using OneGate.Backend.Core.Users.Api.Contracts.Portfolio;

namespace OneGate.Backend.Core.Users.Api.Client
{
    public class UsersApiClient : IUsersApiClient
    {
        private readonly Uri _baseUrl;

        public UsersApiClient(IOptions<UsersApiClientOptions> options)
        {
            _baseUrl = options.Value.BaseUri;
        }

        public async Task<AccountDto> CreateAccountAsync(CreateAccountDto request)
        {
            var result = await _baseUrl
                .AppendPathSegment("accounts")
                .PostJsonAsync(request)
                .ReceiveJson<AccountDto>();

            return result;
        }

        public async Task<IEnumerable<AccountDto>> GetAccountsAsync(FilterAccountsDto request)
        {
            var result = await _baseUrl
                .AppendPathSegment("accounts")
                .SetQueryParamsFromModel(request)
                .GetJsonAsync<IEnumerable<AccountDto>>();

            return result;
        }

        public async Task DeleteAccountAsync(int id)
        {
            await _baseUrl
                .AppendPathSegment("accounts")
                .AppendPathSegment(id)
                .DeleteAsync();
        }

        public async Task<AdministratorDto> CreateAdministratorAsync(CreateAdministratorDto request)
        {
            var result = await _baseUrl
                .AppendPathSegment("administrators")
                .PostJsonAsync(request)
                .ReceiveJson<AdministratorDto>();

            return result;
        }

        public async Task<IEnumerable<AdministratorDto>> GetAdministratorsAsync(FilterAdministratorsDto request)
        {
            var result = await _baseUrl
                .AppendPathSegment("administrators")
                .SetQueryParamsFromModel(request)
                .GetJsonAsync<IEnumerable<AdministratorDto>>();

            return result;
        }

        public async Task DeleteAdministratorAsync(int id)
        {
            await _baseUrl
                .AppendPathSegment("administrators")
                .AppendPathSegment(id)
                .DeleteAsync();
        }

        public async Task<int> CreateOrderAsync(OrderDto request)
        {
            var result = await _baseUrl
                .AppendPathSegment("orders")
                .PostJsonAsync(request)
                .ReceiveJson<int>();

            return result;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersAsync(FilterOrdersDto request)
        {
            var result = await _baseUrl
                .AppendPathSegment("orders")
                .SetQueryParamsFromModel(request)
                .GetJsonAsync<IEnumerable<OrderDto>>();

            return result;
        }

        public async Task DeleteOrderAsync(int id, int ownerId)
        {
            await _baseUrl
                .AppendPathSegment("orders")
                .AppendPathSegment(id)
                .SetQueryParam("owner_id", ownerId)
                .DeleteAsync();
        }

        public async Task<int> CreatePortfolioAsync(PortfolioDto request)
        {
            var result = await _baseUrl
                .AppendPathSegment("portfolios")
                .PostJsonAsync(request)
                .ReceiveJson<int>();

            return result;
        }

        public async Task<IEnumerable<PortfolioDto>> GetPortfoliosAsync(FilterPortfoliosDto request)
        {
            var result = await _baseUrl
                .AppendPathSegment("portfolios")
                .SetQueryParamsFromModel(request)
                .GetJsonAsync<IEnumerable<PortfolioDto>>();

            return result;
        }

        public async Task DeletePortfolioAsync(int id, int ownerId)
        {
            await _baseUrl
                .AppendPathSegment("portfolios")
                .AppendPathSegment(id)
                .SetQueryParam("owner_id", ownerId)
                .DeleteAsync();
        }
    }
}