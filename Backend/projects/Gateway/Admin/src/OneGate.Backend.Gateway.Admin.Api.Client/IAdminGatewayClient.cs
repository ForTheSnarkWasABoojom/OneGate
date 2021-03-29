using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Backend.Gateway.Admin.Api.Contracts.Account;
using OneGate.Backend.Gateway.Admin.Api.Contracts.Administrator;
using OneGate.Backend.Gateway.Admin.Api.Contracts.Credentials;

namespace OneGate.Backend.Gateway.Admin.Api.Client
{
    public interface IAdminGatewayClient
    {
        public Task<IEnumerable<AccountModel>> GetAccountsRangeAsync(FilterAccountsRequest request);
        public Task<AccountModel> GetAccountAsync(int id);
        public Task DeleteAccountAsync(int id);
        public Task CreateAdministratorAsync(CreateAdministratorRequest request);
        public Task<IEnumerable<AdministratorModel>> GetAdministratorsRangeAsync(FilterAdministratorsRequest request);

        public Task DeleteAdministratorAsync(int id);
        public Task CreateTokenAsync(AuthRequest request);
    }
}