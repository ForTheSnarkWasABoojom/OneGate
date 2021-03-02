using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OneGate.Backend.Core.Users.Api.Client;
using OneGate.Backend.Core.Users.Api.Contracts.Account;
using OneGate.Backend.Gateway.Shared.Api;
using OneGate.Backend.Gateway.Shared.Api.Authentication;
using OneGate.Backend.Gateway.Shared.Api.Options;
using OneGate.Backend.Gateway.Shared.Api.Utils;
using OneGate.Backend.Gateway.User.Api.Contracts.Credentials;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.User.Api.Controllers
{
    [ApiVersion("1")]
    [AllowAnonymous]
    [Route(RouteBase + "credentials")]
    public class CredentialsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CredentialsController> _logger;
        private readonly IHashProvider _hashProvider;
        private readonly IUsersApiClient _usersApiClient;
        
        private readonly JwtOptions _authenticationOptions;

        public CredentialsController(ILogger<CredentialsController> logger,
            IOptions<JwtOptions> authenticationOptions, IUsersApiClient usersApiClient, 
            IMapper mapper, IHashProvider hashProvider)
        {
            _logger = logger;
            _usersApiClient = usersApiClient;
            _mapper = mapper;
            _hashProvider = hashProvider;
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
            
            var passwordHash = _hashProvider.Hash(request.Password);
            var payload = await _usersApiClient.GetAccountsAsync(new FilterAccountsDto
            {
                Email = request.Username,
                Password = passwordHash
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