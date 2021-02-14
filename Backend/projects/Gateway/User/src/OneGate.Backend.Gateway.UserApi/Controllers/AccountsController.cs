using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OneGate.Backend.Core.Users.Contracts.Account;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Gateway.Base.Extensions.Claims;
using OneGate.Backend.Gateway.Base.Options;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts;
using OneGate.Shared.ApiModels.User.Account;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.UserApi.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "accounts")]
    public class AccountsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AccountsController> _logger;
        private readonly ITransportBus _bus;

        private readonly AuthenticationOptions _authenticationOptions;

        public AccountsController(ILogger<AccountsController> logger, ITransportBus bus,
            IOptions<AuthenticationOptions> authenticationOptions, IMapper mapper)
        {
            _logger = logger;

            _bus = bus;
            _mapper = mapper;

            _authenticationOptions = authenticationOptions.Value;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation("Create new account")]
        public async Task<IActionResult> CreateAccountAsync([FromBody] CreateAccountModel request)
        {
            if (request.ClientFingerprint != _authenticationOptions.ClientFingerprint)
                return Challenge();

            var accountDto = _mapper.Map<AccountDto>(request);
            await _bus.Call<CreateAccount, CreatedResourceResponse>(new CreateAccount
            {
                Account = accountDto,
                Password = request.Password
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
                Id = User.GetAccountId()
            });
            var accountDto = payload.Accounts.FirstOrDefault();
            
            var account = _mapper.Map<AccountDto, AccountModel>(accountDto);
            return StrictOk(account);
        }
    }
}