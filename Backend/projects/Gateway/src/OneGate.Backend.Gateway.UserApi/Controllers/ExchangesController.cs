using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Exchange;
using OneGate.Shared.ApiContracts.Exchange;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.UserApi.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "exchanges")]
    public class ExchangesController : BaseController
    {
        private readonly ILogger<ExchangesController> _logger;
        private readonly IOgBus _bus;

        public ExchangesController(ILogger<ExchangesController> logger, IOgBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ExchangeDto>), StatusCodes.Status200OK)]
        [SwaggerOperation("Search exchanges")]
        public async Task<IEnumerable<ExchangeDto>> GetExchangesRangeAsync([FromQuery] ExchangeFilterDto request)
        {
            var payload = await _bus.Call<GetExchanges, ExchangesResponse>(new GetExchanges
            {
                Filter = request
            });

            return payload.Exchanges;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ExchangeDto), StatusCodes.Status200OK)]
        [SwaggerOperation("Exchange details")]
        [Route("{id}")]
        public async Task<ExchangeDto> GetExchangeAsync([FromRoute] int id)
        {
            var payload = await _bus.Call<GetExchanges, ExchangesResponse>(new GetExchanges
            {
                Filter = new ExchangeFilterDto
                {
                    Id = id
                }
            });

            return payload.Exchanges.First();
        }
    }
}