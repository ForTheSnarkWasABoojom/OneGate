﻿using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OneGate.Backend.Transport.Dto.Account;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Gateway.Base.Extensions.Claims;
using OneGate.Backend.Gateway.Base.Options;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Account;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Shared.ApiModels.Account;
using OneGate.Shared.ApiModels.Common;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.UserApi.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "accounts")]
    public class AccountsController : BaseController
    {
        private readonly ILogger<AccountsController> _logger;
        
        private readonly IMapper _mapper;
        private readonly IOgBus _bus;

        private readonly AuthenticationOptions _authenticationOptions;
        
        public AccountsController(ILogger<AccountsController> logger, IMapper mapper, IOgBus bus, IOptions<AuthenticationOptions> authenticationOptions)
        {
            _logger = logger;
            
            _mapper = mapper;
            _bus = bus;
            
            _authenticationOptions = authenticationOptions.Value;
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResourceModel), StatusCodes.Status200OK)]
        [SwaggerOperation("Create new account")]
        public async Task<IActionResult> CreateAccountAsync([FromBody] CreateAccountModel request)
        {
            if (request.ClientFingerprint != _authenticationOptions.ClientFingerprint)
                throw new ApiException("Invalid client key", StatusCodes.Status403Forbidden);

            var createAccountDto = _mapper.Map<CreateAccountModel, CreateAccountDto>(request);
            var payload = await _bus.Call<CreateAccount, CreatedResourceResponse>(new CreateAccount
            {
                Account = createAccountDto
            });

            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(AccountModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation("Current account details")]
        public async Task<IActionResult> GetCurrentAccountAsync()
        {
            var payload = await _bus.Call<GetAccounts, AccountsResponse>(new GetAccounts
            {
                Filter = new AccountFilterDto
                {
                    Id = User.GetAccountId()
                }
            });

            var response = _mapper.Map<AccountDto, AccountModel>(payload.Accounts.FirstOrDefault());
            return StrictOk(response);
        }
    }
}