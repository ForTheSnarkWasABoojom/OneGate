using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Core.Shared.Api;
using OneGate.Backend.Core.Shared.Database.Repository;
using OneGate.Backend.Core.Shared.Linq;
using OneGate.Backend.Core.Users.Api.Contracts.Account;
using OneGate.Backend.Core.Users.Database.Models;
using OneGate.Backend.Core.Users.Database.Repository;

namespace OneGate.Backend.Core.Users.Api.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "accounts")]
    public class AccountsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AccountsController> _logger;
        private readonly IAccountRepository _accounts;

        public AccountsController(ILogger<AccountsController> logger, IMapper mapper, IAccountRepository accounts)
        {
            _logger = logger;
            _mapper = mapper;
            _accounts = accounts;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccountAsync([FromBody] CreateAccountDto request)
        {
            var account = _mapper.Map<Account>(request);
            await _accounts.AddAsync(account);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAccountsAsync([FromQuery] FilterAccountsDto request)
        {
            Expression<Func<Account, bool>> filter = p => true;
            var limits = new QueryLimits(request.Shift, request.Count);

            filter = filter
                .FilterBy(p => p.Id == request.Id, request.Id)
                .FilterBy(p => p.Email == request.Email, request.Email)
                .FilterBy(p => p.Password == request.Password, request.Password);

            var accounts = await _accounts.FilterAsync(filter, limits: limits);

            var accountsDto = _mapper.Map<IEnumerable<AccountDto>>(accounts);
            return Ok(accountsDto);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAccountAsync([FromRoute] int id)
        {
            await _accounts.RemoveAsync(p =>
                p.Id == id
            );

            return Ok();
        }
    }
}