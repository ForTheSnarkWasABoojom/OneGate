using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Database;
using OneGate.Backend.Database.Models;
using OneGate.Backend.Rpc;
using OneGate.Backend.Rpc.Contracts.Account.CreateAccount;
using OneGate.Backend.Rpc.Contracts.Account.CreateToken;
using OneGate.Backend.Rpc.Contracts.Account.DeleteAccount;
using OneGate.Backend.Rpc.Contracts.Account.GetAccount;
using OneGate.Backend.Rpc.Contracts.Account.GetAccountsByFilter;
using OneGate.Backend.Rpc.Contracts.Base.HealthCheck;
using OneGate.Backend.Rpc.Services;
using OneGate.Shared.Models.Account;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace OneGate.Backend.AccountService
{
    public class AccountService : IHostedService, IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly IBus _bus;

        public AccountService(ILogger<AccountService> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;

            // Register method as remote callable.
            _bus.RegisterMethodAsync<HealthCheckRequest, HealthCheckResponse>(HealthCheckAsync);
            _bus.RegisterMethodAsync<CreateTokenRequest, CreateTokenResponse>(CreateTokenAsync);
            _bus.RegisterMethodAsync<CreateAccountRequest, CreateAccountResponse>(CreateAccountAsync);
            _bus.RegisterMethodAsync<GetAccountRequest, GetAccountResponse>(GetAccountAsync);
            _bus.RegisterMethodAsync<DeleteAccountRequest, DeleteAccountResponse>(DeleteAccountAsync);
            _bus.RegisterMethodAsync<GetAccountsByFilterRequest, GetAccountsByFilterResponse>(GetAccountsByFilterAsync);
        }

        public async Task<HealthCheckResponse> HealthCheckAsync(HealthCheckRequest request)
        {
            return new HealthCheckResponse
            {
                Timestamp = DateTime.Now
            };
        }

        public async Task<CreateTokenResponse> CreateTokenAsync(CreateTokenRequest request)
        {
            await using var db = new DatabaseContext();
            
            var account = await db.Accounts.FirstOrDefaultAsync(x =>
                x.Password == GetHash(request.Password) &&
                x.Email == request.Email);

            if (account is null)
                throw new ApiException("Account with specified username and password does not exist",
                    Status401Unauthorized);

            return new CreateTokenResponse
            {
                Account = ConvertAccountToDto(account),
                IsAdmin = account.IsAdmin
            };
        }

        public async Task<CreateAccountResponse> CreateAccountAsync(CreateAccountRequest request)
        {
            await using var db = new DatabaseContext();
            
            if (await db.Accounts.FirstOrDefaultAsync(x => x.Email == request.Account.Email) != null)
                throw new ApiException("Account must have unique email", Status400BadRequest);

            var account = await db.Accounts.AddAsync(new Account
            {
                FirstName = request.Account.FirstName,
                LastName = request.Account.LastName,
                Email = request.Account.Email,
                Password = GetHash(request.Account.Password)
            });
            await db.SaveChangesAsync();

            return new CreateAccountResponse()
            {
                Account = ConvertAccountToDto(account.Entity),
            };
        }

        public async Task<GetAccountResponse> GetAccountAsync(GetAccountRequest request)
        {
            await using var db = new DatabaseContext();
            
            var account = await db.Accounts.FindAsync(request.Id);
            if (account is null)
                throw new ApiException("Account with specified id does not exist", Status404NotFound);

            return new GetAccountResponse
            {
                Account = ConvertAccountToDto(account)
            };
        }

        public async Task<DeleteAccountResponse> DeleteAccountAsync(DeleteAccountRequest request)
        {
            await using var db = new DatabaseContext();
            
            var account = await db.Accounts.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (account == null)
                throw new ApiException("Account with specified id does not exist", Status404NotFound);

            db.Accounts.Remove(account);
            await db.SaveChangesAsync();

            return new DeleteAccountResponse();
        }

        public async Task<GetAccountsByFilterResponse> GetAccountsByFilterAsync(GetAccountsByFilterRequest request)
        {
            await using var db = new DatabaseContext();
            
            var accountsQuery = db.Accounts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Filter.FirstName))
                accountsQuery = accountsQuery.Where(x => x.FirstName.Contains(request.Filter.FirstName, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(request.Filter.LastName))
                accountsQuery = accountsQuery.Where(x => x.LastName.Contains(request.Filter.LastName, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(request.Filter.Email))
                accountsQuery = accountsQuery.Where(x => x.Email.Contains(request.Filter.Email, StringComparison.OrdinalIgnoreCase));

            if (request.Filter.IsAdmin != null)
                accountsQuery = accountsQuery.Where(x => x.IsAdmin == request.Filter.IsAdmin);

            var accounts = await accountsQuery.Skip(request.Filter.Shift).Take(request.Filter.Count).ToListAsync();
            return new GetAccountsByFilterResponse
            {
                Accounts = accounts.Select(ConvertAccountToDto).ToList()
            };
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Account service started");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Account service stopped");
        }

        private AccountDto ConvertAccountToDto(Account account)
        {
            return new AccountDto
            {
                Id = account.Id,
                Email = account.Email,
                FirstName = account.FirstName,
                LastName = account.LastName
            };
        }

        private string GetHash(string str)
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