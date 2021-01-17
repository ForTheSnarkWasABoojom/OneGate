using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Account;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Common.Models.Account;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.AdminApi.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "accounts")]
    public class AccountsController : BaseController
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IOgBus _bus;

        public AccountsController(ILogger<AccountsController> logger, IOgBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AccountDto>), StatusCodes.Status200OK)]
        [SwaggerOperation("Search accounts")]
        public async Task<IEnumerable<AccountDto>> GetAccountsRangeAsync([FromQuery] AccountFilterDto request)
        {
            var payload = await _bus.Call<GetAccounts, AccountsResponse>(new GetAccounts
            {
                Filter = request
            });

            return payload.Accounts;
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
        [SwaggerOperation("Account details")]
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

        [HttpDelete]
        [SwaggerOperation("Delete account")]
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