using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Contracts.Common;
using OneGate.Backend.Contracts.Series.Ohlc;
using OneGate.Backend.Gateway.Middleware;
using OneGate.Backend.Rpc;
using OneGate.Shared.Models.Common;
using OneGate.Shared.Models.Series.Ohlc;
using Swashbuckle.AspNetCore.Annotations;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace OneGate.Backend.Gateway.Controllers
{
    [ApiController]
    [ProducesResponseType(typeof(ErrorDto), Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorDto), Status400BadRequest)]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class OhlcSeriesController : ControllerBase
    {
        private readonly ILogger<OhlcSeriesController> _logger;
        private readonly IOgBus _bus;

        public OhlcSeriesController(ILogger<OhlcSeriesController> logger, IOgBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        [HttpPost, Authorize(GroupPolicies.Admin)]
        [SwaggerOperation("[ADMIN] Create OHLC timeseries range")]
        public async Task CreateOhlcSeriesAsync([FromBody] OhlcSeriesDto request)
        {
            await _bus.Call<CreateOhlcSeries, SuccessResponse>(
                new CreateOhlcSeries()
                {
                    Series = request
                });
        }

        [HttpGet]
        [ProducesResponseType(typeof(OhlcSeriesDto), Status200OK)]
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

        [HttpDelete, Authorize(GroupPolicies.Admin)]
        [SwaggerOperation("[ADMIN] Delete OHLC timeseries range")]
        public async Task DeleteOhlcSeriesAsync([FromQuery] OhlcSeriesFilterDto request)
        {
            await _bus.Call<DeleteOhlcSeries, SuccessResponse>(new DeleteOhlcSeries
            {
                Filter = request
            });
        }
    }
}