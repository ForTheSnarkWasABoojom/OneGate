using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OneGate.Backend.Core.Users.Api.Client;
using OneGate.Backend.Core.Users.Api.Contracts.Account;
using OneGate.Backend.Core.Users.Api.Contracts.Administrator;
using OneGate.Backend.Gateway.Shared;
using OneGate.Backend.Gateway.Shared.Authentication;
using OneGate.Backend.Gateway.Shared.Options;
using OneGate.Shared.ApiModels.Admin.Credentials;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.Admin.Api.Controllers
{
    [ApiVersion("1")]
    [AllowAnonymous]
    [Route(RouteBase + "credentials")]
    public class CredentialsController : BaseController
    {
        private readonly ILogger<CredentialsController> _logger;
        private readonly JwtOptions _authenticationOptions;
        private readonly IUsersApiClient _usersApiClient;

        public CredentialsController(ILogger<CredentialsController> logger,
            IOptions<JwtOptions> authenticationOptions, IUsersApiClient usersApiClient)
        {
            _logger = logger;
            _usersApiClient = usersApiClient;
            _authenticationOptions = authenticationOptions.Value;
        }

        [HttpPost]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [SwaggerOperation("Create authorization token")]
        [Route("auth")]
        public async Task<IActionResult> CreateTokenAsync([FromBody] AuthRequest request)
        {
            if (request.ClientFingerprint != _authenticationOptions.ClientFingerprint)
                return Challenge();

            var payload = await _usersApiClient.GetAdministratorsAsync(new FilterAdministratorsDto
            {
                Email = request.Username,
                Password = request.Password
            });

            var account = payload.FirstOrDefault();
            if (account == null)
                return Challenge();

            var token = JwtBuilder.FromCredentials(_authenticationOptions, account.Id);
            var response = new TokenResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return Ok(response);
        }
    }
}