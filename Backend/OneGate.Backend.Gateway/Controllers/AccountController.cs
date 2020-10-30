using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OneGate.Backend.Gateway.Extensions;
using OneGate.Backend.Gateway.Middleware;
using OneGate.Backend.Rpc;
using OneGate.Backend.Contracts.Account;
using OneGate.Backend.Contracts.Common;
using OneGate.Shared.Models.Account;
using OneGate.Shared.Models.Common;
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
        private readonly IBus _bus;

        public AccountController(ILogger<AccountController> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
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

            var payload = await _bus.Call<GetAccount, AccountResponse>(new GetAccount
            {
                Email = request.Username,
                Password = request.Password
            });

            if (payload.Account is null)
                throw new ApiException("Invalid username or password", Status403Forbidden);

            var token = new JwtSecurityToken(
                claims: new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, payload.Account.Id.ToString()),
                    new Claim(ClaimTypes.Role, payload.Account.IsAdmin ? AuthPolicy.Admin : AuthPolicy.User)
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

            var payload = await _bus.Call<CreateAccount, CreatedResourceResponse>(new CreateAccount
            {
                Account = request
            });

            return payload.Resource;
        }

        [HttpGet]
        [ProducesResponseType(typeof(AccountDto), Status200OK)]
        [SwaggerOperation("Current account details")]
        [Route("me")]
        public async Task<AccountDto> GetMyAccountAsync()
        {
            var payload = await _bus.Call<GetAccount, AccountResponse>(new GetAccount
            {
                Id = User.GetAccountId()
            });

            return payload.Account;
        }

        [HttpGet, Authorize(AuthPolicy.Admin)]
        [ProducesResponseType(typeof(IEnumerable<AccountDto>), Status200OK)]
        [SwaggerOperation("[ADMIN] Search accounts")]
        public async Task<IEnumerable<AccountDto>> GetAccountsRangeAsync([FromQuery] AccountFilterDto request)
        {
            var payload = await _bus.Call<GetAccountsRange, AccountsRangeResponse>(new GetAccountsRange
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
            var payload = await _bus.Call<GetAccount, AccountResponse>(new GetAccount
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
            await _bus.Call<DeleteAccount, SuccessResponse>(new DeleteAccount
            {
                Id = id
            });
        }
    }
}