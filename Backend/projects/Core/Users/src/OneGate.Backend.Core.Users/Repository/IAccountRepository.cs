using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Backend.Transport.Contracts.Account;
using OneGate.Backend.Transport.Dto.Account;

namespace OneGate.Backend.Core.Users.Repository
{
    public interface IAccountRepository
    {
        public Task<AccountDto> AnyMatch(CreateAuthorizationContext context);
        public Task<int> AddAsync(CreateAccountDto model);
        public Task<IEnumerable<AccountDto>> FilterAsync(AccountFilterDto filter);
        public Task RemoveAsync(int id);
    }
}