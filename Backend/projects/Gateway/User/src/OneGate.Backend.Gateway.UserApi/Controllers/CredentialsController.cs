using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OneGate.Backend.Transport.Dto.Account;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Gateway.Base.Authentication;
using OneGate.Backend.Gateway.Base.Options;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Account;
using OneGate.Shared.ApiModels.Account;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.UserApi.Controllers
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
        [ProducesResponseType(typeof(AccessTokenModel), StatusCodes.Status200OK)]
        [SwaggerOperation("Create authorization token")]
        [Route("auth")]
        public async Task<IActionResult> CreateTokenAsync([FromBody] AuthModel request)
        {
            if (request.ClientFingerprint != _authenticationOptions.ClientFingerprint)
                return Challenge();

            var payload = await _bus.Call<CreateAuthorizationContext, AuthorizationResponse>(
                new CreateAuthorizationContext
                {
                    AuthDto = new AuthDto
                    {
                        Username = request.Username,
                        Password = request.Password
                    }
                });

            if (payload.Account == null)
                return Challenge();

            var token = JwtBuilder.FromCredentials(_authenticationOptions, payload.Account.Id);
            var response = new AccessTokenModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return Ok(response);
        }
    }
}