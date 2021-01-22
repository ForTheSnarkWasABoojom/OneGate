using System.Threading.Tasks;
using Flurl;
using OneGate.Shared.ApiModels.Account;
using OneGate.Shared.ApiLibrary.Base;
using OneGate.Shared.ApiModels.Common;

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

        public async Task<string> GetTokenAsync(string username, string password)
        {
            var model = new AuthModel
            {
                Username = username,
                Password = password,
                ClientFingerprint = _clientFingerprint
            };
            var result = await _baseUrl
                .AppendPathSegment("credentials/auth")
                .PostRequestAsync<AuthModel, AccessTokenModel>(model);
            return result.Token;
        }

        public async Task<int> CreateAccountAsync(CreateAccountModel model)
        {
            var result = await _baseUrl
                .AppendPathSegment("accounts")
                .PostRequestAsync<CreateAccountModel, ResourceModel>(model);
            return result.Id;
        }
    }
}