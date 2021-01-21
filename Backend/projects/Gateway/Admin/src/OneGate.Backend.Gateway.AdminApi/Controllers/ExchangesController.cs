using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Transport.Dto.Common;
using OneGate.Backend.Transport.Dto.Exchange;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Exchange;
using OneGate.Shared.ApiModels.Common;
using OneGate.Shared.ApiModels.Exchange;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.AdminApi.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "exchanges")]
    public class ExchangesController : BaseController
    {
        private readonly ILogger<ExchangesController> _logger;

        private readonly IMapper _mapper;
        private readonly IOgBus _bus;

        public ExchangesController(ILogger<ExchangesController> logger, IOgBus bus, IMapper mapper)
        {
            _logger = logger;
            _bus = bus;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResourceModel), StatusCodes.Status200OK)]
        [SwaggerOperation("Create exchange")]
        public async Task<IActionResult> CreateExchangeAsync([FromBody] CreateExchangeModel request)
        {
            var createExchangeDto = _mapper.Map<CreateExchangeModel, CreateExchangeDto>(request);
            var payload = await _bus.Call<CreateExchange, CreatedResourceResponse>(new CreateExchange
            {
                Exchange = createExchangeDto
            });

            return Ok();
        }

        [HttpDelete]
        [SwaggerOperation("Delete exchange")]
        [Route("{id}")]
        public async Task<IActionResult> DeleteExchangeAsync([FromRoute] int id)
        {
            await _bus.Call<DeleteExchange, SuccessResponse>(new DeleteExchange
            {
                Id = id
            });
            
            return Ok();
        }
    }
}