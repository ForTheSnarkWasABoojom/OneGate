using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Backend.Core.Users.Database.Models;
using OneGate.Backend.Transport.Contracts.Account;

namespace OneGate.Backend.Core.Users.Database.Repository
{
    public interface IAccountRepository
    {
        public Task<Account> AddAsync(Account model);
        public Task<IEnumerable<Account>> FilterAsync(int? id, string email, string firstName,
            string lastName, bool? isAdmin, int shift, int count);
        public Task RemoveAsync(int id);
        public Task<Account> FindAsync(string username, string password);
    }
}