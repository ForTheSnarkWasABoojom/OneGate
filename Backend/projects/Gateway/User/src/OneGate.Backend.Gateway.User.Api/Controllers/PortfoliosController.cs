using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Core.Users.Api.Client;
using OneGate.Backend.Core.Users.Api.Contracts.Portfolio;
using OneGate.Backend.Gateway.Shared;
using OneGate.Backend.Gateway.Shared.Extensions.Claims;
using OneGate.Shared.ApiModels.User.Portfolio;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.User.Api.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "portfolios")]
    public class PortfoliosController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PortfoliosController> _logger;
        private readonly IUsersApiClient _usersApiClient;

        public PortfoliosController(ILogger<PortfoliosController> logger, IMapper mapper,
            IUsersApiClient usersApiClient)
        {
            _logger = logger;
            _mapper = mapper;
            _usersApiClient = usersApiClient;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation("Create new portfolio")]
        public async Task<IActionResult> CreatePortfolioAsync([FromBody] CreatePortfolioRequest request)
        {
            var orderDto = _mapper.Map<CreatePortfolioRequest, PortfolioDto>(request);
            orderDto.OwnerId = User.GetAccountId();

            var payload = await _usersApiClient.CreatePortfolioAsync(orderDto);

            return CreatedAtAction(nameof(GetPortfolioAsync), new
            {
                id = payload
            });
        }

        [HttpGet]
        [ActionName(nameof(GetPortfolioAsync))]
        [ProducesResponseType(typeof(Portfolio), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation("Portfolio details")]
        [Route("{id}")]
        public async Task<IActionResult> GetPortfolioAsync([FromRoute] int id)
        {
            var payload = await _usersApiClient.GetPortfoliosAsync(new FilterPortfoliosDto
            {
                Id = id
            });
            var portfolioDto = payload.FirstOrDefault();

            var portfolio = _mapper.Map<PortfolioDto, Portfolio>(portfolioDto);
            return StrictOk(portfolio);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PortfolioDto>), StatusCodes.Status200OK)]
        [SwaggerOperation("Search portfolios")]
        public async Task<IActionResult> GetPortfoliosRangeAsync([FromQuery] FilterPortfoliosRequest request)
        {
            var filter = _mapper.Map<FilterPortfoliosRequest, FilterPortfoliosDto>(request);
            filter.OwnerId = User.GetAccountId();
            var payload = await _usersApiClient.GetPortfoliosAsync(filter);

            var portfolios = _mapper.Map<IEnumerable<PortfolioDto>, IEnumerable<Portfolio>>(payload);
            return Ok(portfolios);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation("Delete existing portfolio")]
        [Route("{id}")]
        public async Task<IActionResult> DeletePortfolioAsync([FromRoute] int id)
        {
            await _usersApiClient.DeletePortfolioAsync(id, User.GetAccountId());

            return Ok();
        }
    }
}