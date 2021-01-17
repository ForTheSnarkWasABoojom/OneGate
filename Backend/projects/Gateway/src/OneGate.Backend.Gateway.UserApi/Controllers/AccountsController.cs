using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Gateway.Base.Extensions.Claims;
using OneGate.Backend.Gateway.Base.Options;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Account;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Common.Models.Account;
using OneGate.Common.Models.Common;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.UserApi.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "accounts")]
    public class AccountsController : BaseController
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IOgBus _bus;

        private readonly AuthenticationOptions _authenticationOptions;
        
        public AccountsController(ILogger<AccountsController> logger, IOgBus bus, IOptions<AuthenticationOptions> authenticationOptions)
        {
            _logger = logger;
            _bus = bus;
            _authenticationOptions = authenticationOptions.Value;
        }

        [HttpGet]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
        [SwaggerOperation("Current account details")]
        public async Task<AccountDto> GetAccountAsync()
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
        
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResourceDto), StatusCodes.Status200OK)]
        [SwaggerOperation("Create new account")]
        public async Task<ResourceDto> CreateAccountAsync([FromBody] CreateAccountDto request)
        {
            if (request.ClientFingerprint != _authenticationOptions.ClientFingerprint)
                throw new ApiException("Invalid client key", StatusCodes.Status403Forbidden);

            var payload = await _bus.Call<CreateAccount, CreatedResourceResponse>(new CreateAccount
            {
                Account = request
            });

            return payload.Resource;
        }
    }
}