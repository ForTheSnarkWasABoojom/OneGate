using System.Threading.Tasks;
using OneGate.Shared.ApiModels.User.Account;
using OneGate.Shared.ApiModels.User.Credentials;

namespace OneGate.Shared.ApiLibrary.User
{
    public interface IGuestApi
    {
        public Task<TokenResponse> GetTokenAsync(AuthRequest request);
        public Task CreateAccountAsync(CreateAccountRequest request);
    }
}