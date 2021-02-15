using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Core.Base.Contracts;
using OneGate.Backend.Core.Users.Contracts.Portfolio;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Gateway.Base.Extensions.Claims;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Bus.Contracts;
using OneGate.Shared.ApiModels.User.Portfolio;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.UserApi.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "portfolios")]
    public class PortfoliosController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PortfoliosController> _logger;
        private readonly ITransportBus _bus;

        public PortfoliosController(ILogger<PortfoliosController> logger, ITransportBus bus, IMapper mapper)
        {
            _logger = logger;
            _bus = bus;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation("Create new portfolio")]
        public async Task<IActionResult> CreatePortfolioAsync([FromBody] CreatePortfolioModel request)
        {
            var orderDto = _mapper.Map<CreatePortfolioModel, PortfolioDto>(request);
            orderDto.OwnerId = User.GetAccountId();
            
            var payload = await _bus.RequestAsync<CreatePortfolio, CreatedResourceResponse>(new CreatePortfolio
            {
                Portfolio = orderDto
            });

            return CreatedAtAction(nameof(GetPortfolioAsync), new
            {
                id = payload.Id
            });
        }

        [HttpGet]
        [ActionName(nameof(GetPortfolioAsync))]
        [ProducesResponseType(typeof(PortfolioModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation("Portfolio details")]
        [Route("{id}")]
        public async Task<IActionResult> GetPortfolioAsync([FromRoute] int id)
        {
            var payload = await _bus.RequestAsync<GetPortfolios, PortfoliosResponse>(new GetPortfolios
            {
                Id = id
            });
            var portfolioDto = payload.Portfolios.FirstOrDefault();

            var portfolio = _mapper.Map<PortfolioDto, PortfolioModel>(portfolioDto);
            return StrictOk(portfolio);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PortfolioDto>), StatusCodes.Status200OK)]
        [SwaggerOperation("Search portfolios")]
        public async Task<IActionResult> GetPortfoliosRangeAsync([FromQuery] PortfolioFilterModel request)
        {
            var payload = await _bus.RequestAsync<GetPortfolios, PortfoliosResponse>(new GetPortfolios
            {
                Id = request.Id,
                OwnerId = User.GetAccountId(),
                Shift = request.Shift,
                Count = request.Count
            });
            var portfoliosDto = payload.Portfolios;

            var portfolios = _mapper.Map<IEnumerable<PortfolioDto>, IEnumerable<PortfolioModel>>(portfoliosDto);
            return Ok(portfolios);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation("Delete existing portfolio")]
        [Route("{id}")]
        public async Task<IActionResult> DeletePortfolioAsync([FromRoute] int id)
        {
            var payload = await _bus.RequestAsync<DeletePortfolio, SuccessResponse>(new DeletePortfolio
            {
                Id = id,
                OwnerId = User.GetAccountId()
            });

            return Ok();
        }
    }
}