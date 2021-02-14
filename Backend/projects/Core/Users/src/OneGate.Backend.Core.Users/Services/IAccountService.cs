using System.Threading.Tasks;
using OneGate.Backend.Core.Users.Contracts.Account;
using OneGate.Backend.Core.Users.Contracts.Credentials;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Bus.Contracts;
using OneGate.Backend.Transport.Contracts;

namespace OneGate.Backend.Core.Users.Services
{
    public interface IAccountService
    {
        public Task<CreatedResourceResponse> CreateAccountAsync(CreateAccount request);
        public Task<AccountsResponse> GetAccountsAsync(GetAccounts request);
        public Task<SuccessResponse> DeleteAccountAsync(DeleteAccount request);
        public Task<AuthorizationResponse> CreateAuthorizationAsync(CreateAuthorization request);
    }
}