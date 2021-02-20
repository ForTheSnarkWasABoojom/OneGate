using System;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using OneGate.Shared.ApiLibrary.Base;
using OneGate.Shared.ApiModels.User.Account;
using OneGate.Shared.ApiModels.User.Credentials;

namespace OneGate.Shared.ApiLibrary.User
{
    public class GuestApi : IGuestApi
    {
        private readonly Uri _baseUrl;

        public GuestApi(IOptions<ApiClientOptions> options)
        {
            _baseUrl = options.Value.BaseUri;
        }

        public async Task<TokenResponse> GetTokenAsync(AuthRequest request)
        {
            return await _baseUrl
                .AppendPathSegment("credentials/auth")
                .PostJsonAsync(request)
                .ReceiveJson<TokenResponse>();
        }

        public async Task CreateAccountAsync(CreateAccountRequest request)
        {
            await _baseUrl
                .AppendPathSegment("accounts")
                .PostJsonAsync(request);
        }
    }
}