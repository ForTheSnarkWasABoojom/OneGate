using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using OneGate.Backend.Core.Base.Database.Repository;
using OneGate.Backend.Core.Base.Linq;
using OneGate.Backend.Core.Users.Contracts.Account;
using OneGate.Backend.Core.Users.Contracts.Credentials;
using OneGate.Backend.Core.Users.Database.Models;
using OneGate.Backend.Core.Users.Database.Repository;
using OneGate.Backend.Transport.Bus.Contracts;
using OneGate.Backend.Transport.Contracts;

namespace OneGate.Backend.Core.Users.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accounts;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accounts, IMapper mapper)
        {
            _accounts = accounts;
            _mapper = mapper;
        }

        public async Task<CreatedResourceResponse> CreateAccountAsync(CreateAccount request)
        {
            var account = _mapper.Map<AccountDto, Account>(request.Account);
            account.Password = GetHash(request.Password);

            await _accounts.AddAsync(account);

            return new CreatedResourceResponse
            {
                Id = account.Id
            };
        }

        public async Task<AccountsResponse> GetAccountsAsync(GetAccounts request)
        {
            Expression<Func<Account, bool>> filter = p => true;
            var limits = new QueryLimits(request.Shift, request.Count);

            filter
                .FilterBy(p => p.Id == request.Id, request.Id)
                .FilterBy(p => p.Email == request.Email, request.Email);

            var accounts = await _accounts.FilterAsync(filter, limits: limits);

            var accountsDto = _mapper.Map<IEnumerable<Account>, IEnumerable<AccountDto>>(accounts);
            return new AccountsResponse
            {
                Accounts = accountsDto
            };
        }

        public async Task<SuccessResponse> DeleteAccountAsync(DeleteAccount request)
        {
            await _accounts.RemoveAsync(p =>
                p.Id == request.Id
            );

            return new SuccessResponse();
        }

        public async Task<AuthorizationResponse> CreateAuthorizationAsync(CreateAuthorization request)
        {
            Expression<Func<Account, bool>> filter = p => true;

            var hashedPassword = GetHash(request.Password);
            filter
                .FilterBy(p => p.Email == request.Username)
                .FilterBy(p => p.Password == hashedPassword)
                .FilterBy(p => p.IsAdmin == request.IsAdmin, request.IsAdmin);

            var account = await _accounts.FindAsync(filter);

            var accountDto = _mapper.Map<Account, AccountDto>(account);
            return new AuthorizationResponse
            {
                AuthorizedAccount = accountDto
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