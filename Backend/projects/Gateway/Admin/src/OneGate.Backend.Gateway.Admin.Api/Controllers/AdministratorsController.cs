using System.Collections.Generic;
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
using OneGate.Backend.Core.Users.Api.Contracts.Administrator;
using OneGate.Backend.Gateway.Shared;
using OneGate.Backend.Gateway.Shared.Extensions.Claims;
using OneGate.Backend.Gateway.Shared.Options;
using OneGate.Backend.Gateway.Shared.Utils;
using OneGate.Shared.ApiModels.Admin.Administrator;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.Admin.Api.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "administrators")]
    public class AdministratorsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AdministratorsController> _logger;
        private readonly IUsersApiClient _usersApiClient;
        private readonly IHashProvider _hashProvider;

        private readonly JwtOptions _authenticationOptions;

        public AdministratorsController(ILogger<AdministratorsController> logger,
            IOptions<JwtOptions> authenticationOptions, IMapper mapper,
            IUsersApiClient usersApiClient, IHashProvider hashProvider)
        {
            _logger = logger;
            _mapper = mapper;
            _usersApiClient = usersApiClient;
            _hashProvider = hashProvider;
            _authenticationOptions = authenticationOptions.Value;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation("Create new administrator")]
        public async Task<IActionResult> CreateAdministratorAsync([FromBody] CreateAdministratorRequest request)
        {
            if (request.ClientFingerprint != _authenticationOptions.ClientFingerprint)
                return Challenge();

            var administratorDto = _mapper.Map<CreateAdministratorRequest, CreateAdministratorDto>(request);

            administratorDto.Password = _hashProvider.Hash(administratorDto.Password);
            await _usersApiClient.CreateAdministratorAsync(administratorDto);

            return Ok();
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Administrator>), StatusCodes.Status200OK)]
        [SwaggerOperation("Search administrators")]
        public async Task<IActionResult> GetAdministratorsRangeAsync([FromQuery] FilterAdministratorsRequest request)
        {
            var filter = _mapper.Map<FilterAdministratorsRequest, FilterAdministratorsDto>(request);
            var accounts = await _usersApiClient.GetAdministratorsAsync(filter);

            var accountsModel = _mapper.Map<IEnumerable<AdministratorDto>, IEnumerable<Administrator>>(accounts);

            return Ok(accountsModel);
        }
        
        [HttpDelete]
        [SwaggerOperation("Delete administrator")]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAdministratorAsync([FromRoute] int id)
        {
            await _usersApiClient.DeleteAdministratorAsync(id);

            return Ok();
        }
    }
}