using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Core.Users.Api.Client;
using OneGate.Backend.Core.Users.Api.Contracts.Account;
using OneGate.Backend.Gateway.Shared;
using OneGate.Shared.ApiModels.Admin.Account;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.Admin.Api.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "accounts")]
    public class AccountsController : BaseController
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IMapper _mapper;
        private readonly IUsersApiClient _usersApiClient;

        public AccountsController(ILogger<AccountsController> logger, IMapper mapper, IUsersApiClient usersApiClient)
        {
            _logger = logger;
            _mapper = mapper;
            _usersApiClient = usersApiClient;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Account>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation("Search accounts")]
        public async Task<IActionResult> GetAccountsRangeAsync([FromQuery] FilterAccountsRequest request)
        {
            var filter = _mapper.Map<FilterAccountsRequest, FilterAccountsDto>(request);
            var accounts = await _usersApiClient.GetAccountsAsync(filter);

            var accountsModel = _mapper.Map<IEnumerable<AccountDto>, IEnumerable<Account>>(accounts);

            return Ok(accountsModel);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Account), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation("Account details")]
        [Route("{id}")]
        public async Task<IActionResult> GetAccountAsync([FromRoute] int id)
        {
            var payload = await _usersApiClient.GetAccountsAsync(new FilterAccountsDto
            {
                Id = id
            });
            var account = payload.FirstOrDefault();

            var accountModel = _mapper.Map<AccountDto, Account>(account);
            return StrictOk(accountModel);
        }

        [HttpDelete]
        [SwaggerOperation("Delete account")]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAccountAsync([FromRoute] int id)
        {
            await _usersApiClient.DeleteAccountAsync(id);

            return Ok();
        }
    }
}