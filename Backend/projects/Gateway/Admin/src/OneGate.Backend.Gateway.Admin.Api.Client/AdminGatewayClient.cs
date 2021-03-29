using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using OneGate.Backend.Gateway.Admin.Api.Contracts.Account;
using OneGate.Backend.Gateway.Admin.Api.Contracts.Administrator;
using OneGate.Backend.Gateway.Admin.Api.Contracts.Credentials;

namespace OneGate.Backend.Gateway.Admin.Api.Client
{
    public class AdminGatewayClient : IAdminGatewayClient
    {
        private readonly Uri _baseUrl;

        public AdminGatewayClient(IOptions<AdminGatewayClientOptions> options)
        {
            _baseUrl = options.Value.BaseUri;
        }
        
        public async Task<IEnumerable<AccountModel>> GetAccountsRangeAsync(FilterAccountsRequest request)
        {
            var result = await _baseUrl
                .AppendPathSegment("accounts")
                .SetQueryParamsFromModel(request)
                .GetJsonAsync<IEnumerable<AccountModel>>();

            return result; 
        }
        
        public async Task<AccountModel> GetAccountAsync(int id)
        {
            var result = await _baseUrl
                .AppendPathSegment("accounts")
                .AppendPathSegment(id)
                .GetJsonAsync<AccountModel>();

            return result; 
        }
        
        public async Task DeleteAccountAsync(int id)
        {
            await _baseUrl
                .AppendPathSegment("accounts")
                .AppendPathSegment(id)
                .DeleteAsync();
        }
        
        public async Task CreateAdministratorAsync(CreateAdministratorRequest request)
        {
            var result = await _baseUrl
                .AppendPathSegment("administrators")
                .PostJsonAsync(request);
        }
        
        public async Task<IEnumerable<AdministratorModel>> GetAdministratorsRangeAsync(FilterAdministratorsRequest request)
        {
            var result = await _baseUrl
                .AppendPathSegment("administrators")
                .SetQueryParamsFromModel(request)
                .GetJsonAsync<IEnumerable<AdministratorModel>>();

            return result; 
        }
        
        public async Task DeleteAdministratorAsync(int id)
        {
            await _baseUrl
                .AppendPathSegment("administrators")
                .AppendPathSegment(id)
                .DeleteAsync();
        }
        
        public async Task CreateTokenAsync(AuthRequest request)
        {
            var result = await _baseUrl
                .AppendPathSegment("auth")
                .PostJsonAsync(request);
        }
    }
}