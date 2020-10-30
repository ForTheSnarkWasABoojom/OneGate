using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Backend.Contracts.Common;
using OneGate.Shared.Models.Account;

namespace OneGate.Backend.Services.AccountService.Repository
{
    public interface IAccountRepository
    {
        public Task<int> AddAsync(CreateAccountDto model);
        public Task<AccountDto> FindAsync(int id);
        public Task<AccountDto> FindAsync(string email, string password);
        public Task<IEnumerable<AccountDto>> FilterAsync(AccountFilterDto filter);
        public Task RemoveAsync(int id);
    }
}