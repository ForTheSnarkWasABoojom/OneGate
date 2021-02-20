using System;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using OneGate.Shared.ApiLibrary.Base;
using OneGate.Shared.ApiModels.User.Account;

namespace OneGate.Shared.ApiLibrary.User
{
    public class UserApi : IUserApi
    {
        private readonly string _accessToken;
        private readonly Uri _baseUrl;

        public UserApi(IOptions<ApiClientOptions> options, string accessToken)
        {
            _accessToken = accessToken;
            _baseUrl = options.Value.BaseUri;
        }
        
        public async Task<Account> GetAccount()
        {
            return await _baseUrl
                .AppendPathSegment("accounts")
                .WithOAuthBearerToken(_accessToken)
                .GetJsonAsync<Account>();
        }
    }
}