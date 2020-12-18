using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OneGate.Backend.Gateway.Extensions;
using OneGate.Backend.Gateway.Middleware;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Account;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Common.Models.Account;
using OneGate.Common.Models.Common;
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
        private readonly IOgBus _bus;
        private readonly IAuthCredentials _credentials;

        public AccountController(ILogger<AccountController> logger, IOgBus bus, IAuthCredentials credentials)
        {
            _logger = logger;
            _bus = bus;
            _credentials = credentials;
        }

        [HttpPost, AllowAnonymous]
        [ProducesResponseType(typeof(AccessTokenDto), Status200OK)]
        [SwaggerOperation("Bearer authorization")]
        [Route("auth")]
        public async Task<AccessTokenDto> CreateTokenAsync([FromBody] OAuthDto request,
            [FromQuery] ClientKeyDto clientKey)
        {
            if (clientKey.ClientKey != _credentials.ClientKey)
                throw new ApiException("Invalid client key", Status403Forbidden);

            var payload = await _bus.Call<CreateAuthorizationContext, AuthorizationResponse>(
                new CreateAuthorizationContext
                {
                    AuthDto = new OAuthDto
                    {
                        Username = request.Username,
                        Password = request.Password
                    }
                });

            if (payload.Account == null)
                throw new ApiException("Invalid username or password", Status403Forbidden);

            var account = payload.Account;
            var token = new JwtSecurityToken(
                claims: new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                    new Claim(ClaimTypes.Role, account.IsAdmin ? GroupPolicies.Admin : GroupPolicies.User)
                },
                expires: DateTime.Now + GroupPolicies.ExpirationSpan,
                signingCredentials: new SigningCredentials(GroupPolicies.SecurityKey, SecurityAlgorithms.HmacSha256)
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
            if (clientKey.ClientKey != _credentials.ClientKey)
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
            var payload = await _bus.Call<GetAccounts, AccountsResponse>(new GetAccounts
            {
                Filter = new AccountFilterDto
                {
                    Id = User.GetAccountId()
                }
            });

            return payload.Accounts.First();
        }

        [HttpGet, Authorize(GroupPolicies.Admin)]
        [ProducesResponseType(typeof(IEnumerable<AccountDto>), Status200OK)]
        [SwaggerOperation("[ADMIN] Search accounts")]
        public async Task<IEnumerable<AccountDto>> GetAccountsRangeAsync([FromQuery] AccountFilterDto request)
        {
            var payload = await _bus.Call<GetAccounts, AccountsResponse>(new GetAccounts
            {
                Filter = request
            });

            return payload.Accounts;
        }

        [HttpGet, Authorize(GroupPolicies.Admin)]
        [ProducesResponseType(typeof(AccountDto), Status200OK)]
        [SwaggerOperation("[ADMIN] Account details")]
        [Route("{id}")]
        public async Task<AccountDto> GetAccountAsync([FromRoute] int id)
        {
            var payload = await _bus.Call<GetAccounts, AccountsResponse>(new GetAccounts
            {
                Filter = new AccountFilterDto
                {
                    Id = id
                }
            });

            return payload.Accounts.First();
        }

        [HttpDelete, Authorize(GroupPolicies.Admin)]
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