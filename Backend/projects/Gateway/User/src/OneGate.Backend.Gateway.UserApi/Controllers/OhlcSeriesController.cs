using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Transport.Dto.Series.Ohlc;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Series.Ohlc;
using OneGate.Shared.ApiModels.Series.Ohlc;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.UserApi.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "ohlc_series")]
    public class OhlcSeriesController : BaseController
    {
        private readonly ILogger<OhlcSeriesController> _logger;

        private readonly IMapper _mapper;
        private readonly IOgBus _bus;

        public OhlcSeriesController(ILogger<OhlcSeriesController> logger, IOgBus bus, IMapper mapper)
        {
            _logger = logger;
            _bus = bus;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(OhlcSeriesModel), StatusCodes.Status200OK)]
        [SwaggerOperation("Get OHLC timeseries by specified filter")]
        public async Task<IActionResult> GetOhlcSeriesAsync([FromQuery] OhlcSeriesFilterModel request)
        {
            var ohlcSeriesFilterDto = _mapper.Map<OhlcSeriesFilterModel, OhlcSeriesFilterDto>(request);
            var payload = await _bus.Call<GetOhlcSeries, OhlcSeriesResponse>(
                new GetOhlcSeries
                {
                    Filter = ohlcSeriesFilterDto
                });

            var response = _mapper.Map<OhlcSeriesDto, OhlcSeriesModel>(payload.Series);
            return Ok(response);
        }
    }
}