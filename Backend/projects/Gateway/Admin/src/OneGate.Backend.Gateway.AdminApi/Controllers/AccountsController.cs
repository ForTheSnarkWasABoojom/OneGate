using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.AdminApi.Converters;
using OneGate.Backend.Transport.Dto.Account;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Account;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Shared.ApiModels.Account;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.AdminApi.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "accounts")]
    public class AccountsController : BaseController
    {
        private readonly ILogger<AccountsController> _logger;

        private readonly IConverter _converter;
        private readonly IOgBus _bus;

        public AccountsController(ILogger<AccountsController> logger, IOgBus bus, IConverter converter)
        {
            _logger = logger;
            _bus = bus;
            _converter = converter;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AccountModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation("Search accounts")]
        public async Task<IActionResult> GetAccountsRangeAsync([FromQuery] AccountFilterModel request)
        {
            var accountFilterDto = _converter.ToDto(request);
            var payload = await _bus.Call<GetAccounts, AccountsResponse>(new GetAccounts
            {
                Filter = accountFilterDto
            });

            var response = payload.Accounts.Select(_converter.FromDto);
            return Ok(response);
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(AccountModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation("Account details")]
        [Route("{id}")]
        public async Task<IActionResult> GetAccountAsync([FromRoute] int id)
        {
            var payload = await _bus.Call<GetAccounts, AccountsResponse>(new GetAccounts
            {
                Filter = new AccountFilterDto
                {
                    Id = id
                }
            });

            var response = _converter.FromDto(payload.Accounts.FirstOrDefault());
            return StrictOk(response);
        }

        [HttpDelete]
        [SwaggerOperation("Delete account")]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAccountAsync([FromRoute] int id)
        {
            await _bus.Call<DeleteAccount, SuccessResponse>(new DeleteAccount
            {
                Id = id
            });

            return Ok();
        }
    }
}