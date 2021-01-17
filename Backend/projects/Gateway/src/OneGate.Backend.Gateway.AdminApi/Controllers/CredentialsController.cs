using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Gateway.Base.Authentication;
using OneGate.Backend.Gateway.Base.Options;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Account;
using OneGate.Common.Models.Account;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.AdminApi.Controllers
{
    [ApiVersion("1")]
    [AllowAnonymous]
    [Route(RouteBase + "credentials")]
    public class CredentialsController : BaseController
    {
        private readonly ILogger<CredentialsController> _logger;
        private readonly IOgBus _bus;

        private readonly AuthenticationOptions _authenticationOptions;

        public CredentialsController(ILogger<CredentialsController> logger, IOgBus bus,
            IOptions<AuthenticationOptions> authenticationOptions)
        {
            _logger = logger;
            _bus = bus;
            _authenticationOptions = authenticationOptions.Value;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AccessTokenDto), StatusCodes.Status200OK)]
        [SwaggerOperation("Get authorization token")]
        [Route("auth")]
        public async Task<AccessTokenDto> GetTokenAsync([FromBody] OAuthDto request)
        {
            if (request.ClientFingerprint != _authenticationOptions.ClientFingerprint)
                throw new ApiException("Invalid client key", StatusCodes.Status403Forbidden);

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
                throw new ApiException("Invalid username or password", StatusCodes.Status403Forbidden);

            var token = JwtBuilder.FromCredentials(_authenticationOptions, payload.Account.Id);
            return new AccessTokenDto
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}