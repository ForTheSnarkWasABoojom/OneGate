using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Core.Users.Contracts.Account;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Bus.Contracts;
using OneGate.Backend.Transport.Contracts;
using OneGate.Shared.ApiModels.Admin.Account;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.AdminApi.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "accounts")]
    public class AccountsController : BaseController
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly ITransportBus _bus;

        private readonly IMapper _mapper;

        public AccountsController(ILogger<AccountsController> logger, ITransportBus bus, IMapper mapper)
        {
            _logger = logger;
            _bus = bus;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AccountModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation("Search accounts")]
        public async Task<IActionResult> GetAccountsRangeAsync([FromQuery] AccountFilterModel request)
        {
            var payload = await _bus.Call<GetAccounts, AccountsResponse>(new GetAccounts
            {
                Id = request.Id,
                Email = request.Email
            });
            var accounts = payload.Accounts;

            var accountsModel = _mapper.Map<IEnumerable<AccountDto>, IEnumerable<AccountModel>>(accounts);
            return Ok(accountsModel);
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
                Id = id
            });
            var account = payload.Accounts.FirstOrDefault();

            var accountModel = _mapper.Map<AccountDto, AccountModel>(account);
            return StrictOk(accountModel);
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