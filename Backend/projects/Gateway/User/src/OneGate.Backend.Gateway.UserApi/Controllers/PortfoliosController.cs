using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Transport.Dto.Portfolio;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Gateway.Base.Extensions.Claims;
using OneGate.Backend.Gateway.UserApi.Converters;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Portfolio;
using OneGate.Shared.ApiModels.Portfolio;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.UserApi.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "portfolios")]
    public class PortfoliosController : BaseController
    {
        private readonly ILogger<PortfoliosController> _logger;

        private readonly IConverter _converter;
        private readonly IOgBus _bus;

        public PortfoliosController(ILogger<PortfoliosController> logger, IOgBus bus, IConverter converter)
        {
            _logger = logger;
            _bus = bus;
            _converter = converter;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation("Create new portfolio")]
        public async Task<IActionResult> CreatePortfolioAsync([FromBody] CreatePortfolioModel request)
        {
            var createPortfolioDto = _converter.ToDto(request);
            var payload = await _bus.Call<CreatePortfolio, CreatedResourceResponse>(new CreatePortfolio
            {
                Portfolio = createPortfolioDto,
                OwnerId = User.GetAccountId()
            });

            return CreatedAtAction(nameof(GetPortfolioAsync), new {id = payload.Resource.Id});
        }

        [HttpGet]
        [ActionName(nameof(GetPortfolioAsync))]
        [ProducesResponseType(typeof(PortfolioModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation("Portfolio details")]
        [Route("{id}")]
        public async Task<IActionResult> GetPortfolioAsync([FromRoute] int id)
        {
            var payload = await _bus.Call<GetPortfolios, PortfoliosResponse>(new GetPortfolios
            {
                Filter = new PortfolioFilterDto
                {
                    Id = id
                }
            });

            var response = _converter.FromDto(payload.Portfolios.FirstOrDefault());
            return StrictOk(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PortfolioDto>), StatusCodes.Status200OK)]
        [SwaggerOperation("Search portfolios")]
        public async Task<IActionResult> GetPortfoliosRangeAsync([FromQuery] PortfolioFilterDto request)
        {
            var payload = await _bus.Call<GetPortfolios, PortfoliosResponse>(new GetPortfolios
            {
                Filter = request,
                OwnerId = User.GetAccountId()
            });

            var response = payload.Portfolios.Select(_converter.FromDto);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation("Delete existing portfolio")]
        [Route("{id}")]
        public async Task<IActionResult> DeletePortfolioAsync([FromRoute] int id)
        {
            var payload = await _bus.Call<DeletePortfolio, SuccessResponse>(new DeletePortfolio
            {
                Id = id,
                OwnerId = User.GetAccountId()
            });

            return Ok();
        }
    }
}