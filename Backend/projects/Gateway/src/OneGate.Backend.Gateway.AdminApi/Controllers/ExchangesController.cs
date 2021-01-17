using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Exchange;
using OneGate.Shared.ApiContracts.Common;
using OneGate.Shared.ApiContracts.Exchange;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.AdminApi.Controllers
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

        [HttpPost]
        [ProducesResponseType(typeof(ResourceDto), StatusCodes.Status200OK)]
        [SwaggerOperation("Create exchange")]
        public async Task<ResourceDto> CreateExchangeAsync([FromBody] CreateExchangeDto request)
        {
            var payload = await _bus.Call<CreateExchange, CreatedResourceResponse>(new CreateExchange
            {
                Exchange = request
            });

            return payload.Resource;
        }

        [HttpDelete]
        [SwaggerOperation("Delete exchange")]
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