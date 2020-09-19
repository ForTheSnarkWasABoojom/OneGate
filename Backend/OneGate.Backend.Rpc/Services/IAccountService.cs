using System.Threading.Tasks;
using EasyNetQ;
using OneGate.Backend.Rpc.Contracts.Account.CreateAccount;
using OneGate.Backend.Rpc.Contracts.Account.CreateToken;
using OneGate.Backend.Rpc.Contracts.Account.DeleteAccount;
using OneGate.Backend.Rpc.Contracts.Account.GetAccount;
using OneGate.Backend.Rpc.Contracts.Account.GetAccountsByFilter;
using OneGate.Backend.Rpc.Contracts.Base.HealthCheck;

namespace OneGate.Backend.Rpc.Services
{
    public interface IAccountService
    {
        public Task<HealthCheckResponse> HealthCheckAsync(HealthCheckRequest request);
        public Task<CreateTokenResponse> CreateTokenAsync(CreateTokenRequest request);
        
        public Task<CreateAccountResponse> CreateAccountAsync(CreateAccountRequest request);
        public Task<GetAccountResponse> GetAccountAsync(GetAccountRequest request);
        public Task<DeleteAccountResponse> DeleteAccountAsync(DeleteAccountRequest request);
        public Task<GetAccountsByFilterResponse> GetAccountsByFilterAsync(GetAccountsByFilterRequest request);
    }

    public class AccountService : IAccountService
    {
        private IBus _bus;
        
        public AccountService(IBus bus)
        {
            _bus = bus;
        }

        public async Task<HealthCheckResponse> HealthCheckAsync(HealthCheckRequest request)
        {
            return await _bus.CallAsync<HealthCheckRequest, HealthCheckResponse>(request);
        }

        public async Task<CreateTokenResponse> CreateTokenAsync(CreateTokenRequest request)
        {
            return await _bus.CallAsync<CreateTokenRequest, CreateTokenResponse>(request);
        }
        
        public async Task<CreateAccountResponse> CreateAccountAsync(CreateAccountRequest request)
        {
            return await _bus.CallAsync<CreateAccountRequest, CreateAccountResponse>(request);
        }

        public async Task<GetAccountResponse> GetAccountAsync(GetAccountRequest request)
        {
            return await _bus.CallAsync<GetAccountRequest, GetAccountResponse>(request);
        }

        public async Task<DeleteAccountResponse> DeleteAccountAsync(DeleteAccountRequest request)
        {
            return await _bus.CallAsync<DeleteAccountRequest, DeleteAccountResponse>(request);
        }

        public async Task<GetAccountsByFilterResponse> GetAccountsByFilterAsync(GetAccountsByFilterRequest request)
        {
            return await _bus.CallAsync<GetAccountsByFilterRequest, GetAccountsByFilterResponse>(request);
        }
    }
}