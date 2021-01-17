using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Core.Users.Database.Models;

namespace OneGate.Backend.Core.Users.Database.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DatabaseContext _db;

        public AccountRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<Account> AddAsync(Account model)
        {
            var account = await _db.Accounts.AddAsync(model);

            await _db.Portfolios.AddAsync(new Portfolio
            {
                Name = "Default",
                Owner = account.Entity
            });

            await _db.SaveChangesAsync();
            return account.Entity;
        }

        public async Task<IEnumerable<Account>> FilterAsync(int? id, string email, string firstName,
            string lastName, bool? isAdmin, int shift, int count)
        {
            var accountsQuery = _db.Accounts.AsQueryable();

            if (id != null)
                accountsQuery = accountsQuery.Where(x => x.Id == id);

            if (!string.IsNullOrWhiteSpace(firstName))
                accountsQuery = accountsQuery.Where(x =>
                    x.FirstName.ToLower().Contains(firstName.ToLower()));

            if (!string.IsNullOrWhiteSpace(lastName))
                accountsQuery = accountsQuery.Where(x =>
                    x.LastName.ToLower().Contains(lastName.ToLower()));

            if (!string.IsNullOrWhiteSpace(email))
                accountsQuery = accountsQuery.Where(x =>
                    x.Email.ToLower().Contains(email.ToLower()));

            if (isAdmin != null)
                accountsQuery = accountsQuery.Where(x => x.IsAdmin == isAdmin);
            
            return await accountsQuery.Skip(shift).Take(count).ToListAsync();
        }

        public async Task RemoveAsync(int id)
        {
            _db.Accounts.RemoveRange(_db.Accounts.Where(x => x.Id == id));
            await _db.SaveChangesAsync();
        }
        
        public async Task<Account> FindAsync(string username, string password)
        {
            var account = _db.Accounts.First(x =>
                x.Email == username && x.Password == GetHash(password));
            return account;
        }
        
        private static string GetHash(string str)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: str,
                salt: new byte[128 / 8],
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }
    }
}