using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Core.Assets.Api.Client;
using OneGate.Backend.Core.Assets.Api.Contracts.Exchange;
using OneGate.Backend.Gateway.Shared.Api;
using OneGate.Backend.Gateway.User.Api.Contracts.Exchange;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.User.Api.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "exchanges")]
    public class ExchangesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ExchangesController> _logger;
        private readonly IAssetsApiClient _assetsApiClient;

        public ExchangesController(ILogger<ExchangesController> logger, IMapper mapper,
            IAssetsApiClient assetsApiClient)
        {
            _logger = logger;
            _mapper = mapper;
            _assetsApiClient = assetsApiClient;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ExchangeModel>), StatusCodes.Status200OK)]
        [SwaggerOperation("Get exchanges by specified filter")]
        public async Task<IActionResult> GetExchangesRangeAsync([FromQuery] FilterExchangesRequest request)
        {
            var filter = _mapper.Map<FilterExchangesRequest, FilterExchangesDto>(request);
            var payload = await _assetsApiClient.GetExchangesAsync(filter);

            var exchanges = _mapper.Map<IEnumerable<ExchangeDto>, IEnumerable<ExchangeModel>>(payload);
            return Ok(exchanges);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ExchangeModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation("Exchange details")]
        [Route("{id}")]
        public async Task<IActionResult> GetExchangeAsync([FromRoute] int id)
        {
            var payload = await _assetsApiClient.GetExchangesAsync(new FilterExchangesDto
            {
                Id = id
            });
            var exchangeDto = payload.FirstOrDefault();

            var exchange = _mapper.Map<ExchangeDto, ExchangeModel>(exchangeDto);
            return StrictOk(exchange);
        }
    }
}