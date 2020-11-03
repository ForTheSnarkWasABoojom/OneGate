using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Shared.Models.Account;

namespace OneGate.Backend.Services.AccountService.Repository
{
    public interface IAccountRepository
    {
        public Task<int> AddAsync(CreateAccountDto model);
        public Task<IEnumerable<AccountDto>> FilterAsync(AccountFilterDto filter);
        public Task RemoveAsync(int id);
    }
}