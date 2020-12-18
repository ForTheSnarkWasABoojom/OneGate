using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Core.User.Database;
using OneGate.Backend.Core.User.Database.Models;
using OneGate.Backend.Transport.Contracts.Account;
using OneGate.Common.Models.Account;

namespace OneGate.Backend.Core.User.Service.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DatabaseContext _db;

        public AccountRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<AccountDto> AnyMatch(CreateAuthorizationContext context)
        {
            var account = _db.Accounts.First(x =>
                x.Email == context.AuthDto.Username && x.Password == GetHash(context.AuthDto.Password));
            return ConvertAccountToDto(account);
        }

        public async Task<int> AddAsync(CreateAccountDto model)
        {
            var account = new Account
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = GetHash(model.Password)
            };
            await _db.Accounts.AddAsync(account);

            await _db.Portfolios.AddAsync(new Portfolio
            {
                Name = "Default",
                Owner = account
            });

            await _db.SaveChangesAsync();
            return account.Id;
        }

        public async Task<IEnumerable<AccountDto>> FilterAsync(AccountFilterDto filter)
        {
            var accountsQuery = _db.Accounts.AsQueryable();

            if (filter.Id != null)
                accountsQuery = accountsQuery.Where(x => x.Id == filter.Id);

            if (!string.IsNullOrWhiteSpace(filter.FirstName))
                accountsQuery = accountsQuery.Where(x =>
                    x.FirstName.ToLower().Contains(filter.FirstName.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.LastName))
                accountsQuery = accountsQuery.Where(x =>
                    x.LastName.ToLower().Contains(filter.LastName.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Email))
                accountsQuery = accountsQuery.Where(x =>
                    x.Email.ToLower().Contains(filter.Email.ToLower()));

            if (filter.IsAdmin != null)
                accountsQuery = accountsQuery.Where(x => x.IsAdmin == filter.IsAdmin);

            var accounts = await accountsQuery.Skip(filter.Shift).Take(filter.Count).ToListAsync();

            return accounts.Select(ConvertAccountToDto);
        }

        public async Task RemoveAsync(int id)
        {
            _db.Accounts.RemoveRange(_db.Accounts.Where(x => x.Id == id));
            await _db.SaveChangesAsync();
        }

        private static AccountDto ConvertAccountToDto(Account account)
        {
            if (account is null)
                return null;

            return new AccountDto
            {
                Id = account.Id,
                Email = account.Email,
                FirstName = account.FirstName,
                LastName = account.LastName,
                IsAdmin = account.IsAdmin
            };
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