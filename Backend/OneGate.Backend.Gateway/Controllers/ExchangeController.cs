using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Contracts.Common;
using OneGate.Backend.Gateway.Middleware;
using OneGate.Backend.Contracts.Exchange;
using OneGate.Backend.Rpc;
using OneGate.Shared.Models.Common;
using OneGate.Shared.Models.Exchange;
using Swashbuckle.AspNetCore.Annotations;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace OneGate.Backend.Gateway.Controllers
{
    [ApiController]
    [ProducesResponseType(typeof(ErrorDto), Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorDto), Status400BadRequest)]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ExchangeController : ControllerBase
    {
        private readonly ILogger<ExchangeController> _logger;
        private readonly IBus _bus;

        public ExchangeController(ILogger<ExchangeController> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        [HttpPost, Authorize(AuthPolicy.Admin)]
        [ProducesResponseType(typeof(ResourceDto), Status200OK)]
        [SwaggerOperation("[ADMIN] Create exchange")]
        public async Task<ResourceDto> CreateExchangeAsync([FromBody] CreateExchangeDto request)
        {
            var payload = await _bus.Call<CreateExchange, CreatedResourceResponse>(new CreateExchange
            {
                Exchange = request
            });

            return payload.Resource;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ExchangeDto>), Status200OK)]
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
        [ProducesResponseType(typeof(ExchangeDto), Status200OK)]
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

        [HttpDelete, Authorize(AuthPolicy.Admin)]
        [SwaggerOperation("[ADMIN] Delete exchange")]
        [Route("{id}")]
        public async Task DeleteExchangeAsync([FromRoute] int id)
        {
            await _bus.Call<DeleteExchange, SuccessResponse>(new DeleteExchange
            {
                Id = id
            });
        }
    }
}