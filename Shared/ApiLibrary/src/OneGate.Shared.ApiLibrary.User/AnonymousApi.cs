using System.Threading.Tasks;
using Flurl;
using OneGate.Shared.ApiContracts.Account;
using OneGate.Shared.ApiLibrary.Base;

namespace OneGate.Shared.ApiLibrary.User
{
    public class AnonymousApi : IAnonymousApi
    {
        private readonly string _clientFingerprint;
        private readonly string _baseUrl;

        public AnonymousApi(string baseUrl, string clientFingerprint)
        {
            _clientFingerprint = clientFingerprint;
            _baseUrl = baseUrl;
        }

        public async Task<string> GetAccessTokenAsync(string username, string password)
        {
            var model = new OAuthDto
            {
                Username = username,
                Password = password,
                ClientFingerprint = _clientFingerprint
            };
            var result = await _baseUrl
                .AppendPathSegment("credentials/auth")
                .PostRequestAsync<OAuthDto, AccessTokenDto>(model);
            return result.AccessToken;
        }
    }
}