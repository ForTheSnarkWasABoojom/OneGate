using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OneGate.Backend.Gateway.Extensions;
using OneGate.Backend.Gateway.Middleware;
using OneGate.Backend.Rpc;
using OneGate.Backend.Rpc.Contracts.Account.CreateAccount;
using OneGate.Backend.Rpc.Contracts.Account.CreateToken;
using OneGate.Backend.Rpc.Contracts.Account.DeleteAccount;
using OneGate.Backend.Rpc.Contracts.Account.GetAccount;
using OneGate.Backend.Rpc.Contracts.Account.GetAccountsByFilter;
using OneGate.Backend.Rpc.Contracts.Order.CreateOrder;
using OneGate.Backend.Rpc.Contracts.Order.DeleteOrder;
using OneGate.Backend.Rpc.Contracts.Order.GetOrder;
using OneGate.Backend.Rpc.Contracts.Order.GetOrdersByFilter;
using OneGate.Backend.Rpc.Services;
using OneGate.Shared.Models.Account;
using OneGate.Shared.Models.Common;
using OneGate.Shared.Models.Order;
using Swashbuckle.AspNetCore.Annotations;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace OneGate.Backend.Gateway.Controllers
{
    [ApiController]
    [ProducesResponseType(typeof(ErrorDto), Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorDto), Status400BadRequest)]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        [HttpPost, AllowAnonymous]
        [ProducesResponseType(typeof(AccessTokenDto), Status200OK)]
        [SwaggerOperation("Bearer authorization")]
        [Route("auth")]
        public async Task<AccessTokenDto> CreateTokenAsync([FromBody] OAuthDto request,
            [FromQuery] ClientKeyDto clientKey)
        {
            if (clientKey.ClientKey != AuthPolicy.ClientKey)
                throw new ApiException("Invalid client key", Status403Forbidden);

            var payload = await _accountService.CreateTokenAsync(new CreateTokenRequest
            {
                Email = request.Username,
                Password = request.Password
            });

            var token = new JwtSecurityToken(
                claims: new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, payload.Account.Id.ToString()),
                    new Claim(ClaimTypes.Role, payload.IsAdmin ? AuthPolicy.Admin : AuthPolicy.User)
                },
                expires: DateTime.Now + AuthPolicy.ExpirationSpan,
                signingCredentials: new SigningCredentials(AuthPolicy.SecurityKey, SecurityAlgorithms.HmacSha256)
            );

            return new AccessTokenDto
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        [HttpPost, AllowAnonymous]
        [ProducesResponseType(typeof(ResourceDto), Status200OK)]
        [SwaggerOperation("Register new account")]
        public async Task<ResourceDto> CreateAccountAsync([FromBody] CreateAccountDto request,
            [FromQuery] ClientKeyDto clientKey)
        {
            if (clientKey.ClientKey != AuthPolicy.ClientKey)
                throw new ApiException("Invalid client key", Status403Forbidden);

            var payload = await _accountService.CreateAccountAsync(new CreateAccountRequest
            {
                Account = request,
                ClientKey = clientKey.ClientKey
            });

            return payload.Resource;
        }

        [HttpGet]
        [ProducesResponseType(typeof(AccountDto), Status200OK)]
        [SwaggerOperation("Current account details")]
        [Route("me")]
        public async Task<AccountDto> GetMyAccountAsync()
        {
            var payload = await _accountService.GetAccountAsync(new GetAccountRequest
            {
                Id = User.GetAccountId()
            });

            return payload.Account;
        }

        [HttpGet, Authorize(AuthPolicy.Admin)]
        [ProducesResponseType(typeof(List<AccountDto>), Status200OK)]
        [SwaggerOperation("[ADMIN] Search accounts")]
        public async Task<List<AccountDto>> GetAccountsByFilterAsync([FromQuery] AccountFilterDto request)
        {
            var payload = await _accountService.GetAccountsByFilterAsync(new GetAccountsByFilterRequest
            {
                Filter = request
            });

            return payload.Accounts;
        }

        [HttpGet, Authorize(AuthPolicy.Admin)]
        [ProducesResponseType(typeof(AccountDto), Status200OK)]
        [SwaggerOperation("[ADMIN] Account details")]
        [Route("{id}")]
        public async Task<AccountDto> GetAccountAsync([FromRoute] int id)
        {
            var payload = await _accountService.GetAccountAsync(new GetAccountRequest
            {
                Id = id
            });

            return payload.Account;
        }

        [HttpDelete, Authorize(AuthPolicy.Admin)]
        [SwaggerOperation("[ADMIN] Delete account")]
        [Route("{id}")]
        public async Task DeleteAccountAsync([FromRoute] int id)
        {
            await _accountService.DeleteAccountAsync(new DeleteAccountRequest
            {
                Id = id
            });
        }
    }
}