using System.Threading.Tasks;
using Flurl;
using OneGate.Shared.ApiLibrary.Base;
using OneGate.Shared.ApiModels.User.Account;

namespace OneGate.Shared.ApiLibrary.User
{
    public class UserApi : IUserApi
    {
        private readonly string _accessToken;
        private readonly string _baseUrl;

        public UserApi(string baseUrl, string accessToken)
        {
            _accessToken = accessToken;
            _baseUrl = baseUrl;
        }
        
        public async Task<AccountModel> GetMyAccount()
        {
            var result = await _baseUrl
                .AppendPathSegment("accounts")
                .GetRequestAsync<CreateAccountModel, AccountModel>();
            return result;
        }
    }
}