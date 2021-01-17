using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Series.Ohlc;
using OneGate.Shared.ApiContracts.Series.Ohlc;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.UserApi.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "ohlc_series")]
    public class OhlcSeriesController : BaseController
    {
        private readonly ILogger<OhlcSeriesController> _logger;
        private readonly IOgBus _bus;

        public OhlcSeriesController(ILogger<OhlcSeriesController> logger, IOgBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        [HttpGet]
        [ProducesResponseType(typeof(OhlcSeriesDto), StatusCodes.Status200OK)]
        [SwaggerOperation("Search OHLC timeseries range")]
        public async Task<OhlcSeriesDto> GetOhlcSeriesAsync(
            [FromQuery] OhlcSeriesFilterDto request)
        {
            var payload = await _bus.Call<GetOhlcSeries, OhlcSeriesResponse>(
                new GetOhlcSeries
                {
                    Filter = request
                });

            return payload.Series;
        }
    }
}