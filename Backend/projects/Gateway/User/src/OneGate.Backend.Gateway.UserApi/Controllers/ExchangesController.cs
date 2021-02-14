using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Core.Assets.Contracts.Exchange;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Transport.Bus;
using OneGate.Shared.ApiModels.User.Exchange;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.UserApi.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "exchanges")]
    public class ExchangesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ExchangesController> _logger;
        private readonly ITransportBus _bus;

        public ExchangesController(ILogger<ExchangesController> logger, ITransportBus bus, IMapper mapper)
        {
            _logger = logger;
            _bus = bus;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ExchangeModel>), StatusCodes.Status200OK)]
        [SwaggerOperation("Get exchanges by specified filter")]
        public async Task<IActionResult> GetExchangesRangeAsync([FromQuery] ExchangeFilterModel request)
        {
            var payload = await _bus.Call<GetExchanges, ExchangesResponse>(new GetExchanges
            {
                Id = request.Id,
                Shift = request.Shift,
                Count = request.Count
            });
            var exchangesDto = payload.Exchanges;

            var exchanges = _mapper.Map<IEnumerable<ExchangeDto>, IEnumerable<ExchangeModel>>(exchangesDto);
            return Ok(exchanges);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ExchangeModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation("Exchange details")]
        [Route("{id}")]
        public async Task<IActionResult> GetExchangeAsync([FromRoute] int id)
        {
            var payload = await _bus.Call<GetExchanges, ExchangesResponse>(new GetExchanges
            {
                Id = id
            });
            var exchangeDto = payload.Exchanges.FirstOrDefault();

            var exchange = _mapper.Map<ExchangeDto, ExchangeModel>(exchangeDto);
            return StrictOk(exchange);
        }
    }
}