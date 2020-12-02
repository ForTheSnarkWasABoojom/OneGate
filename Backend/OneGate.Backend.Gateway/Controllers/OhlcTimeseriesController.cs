using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Contracts.Common;
using OneGate.Backend.Contracts.OhlcTimeseries;
using OneGate.Backend.Gateway.Middleware;
using OneGate.Backend.Rpc;
using OneGate.Shared.Models.Common;
using OneGate.Shared.Models.Timeseries;
using Swashbuckle.AspNetCore.Annotations;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace OneGate.Backend.Gateway.Controllers
{
    [ApiController]
    [ProducesResponseType(typeof(ErrorDto), Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorDto), Status400BadRequest)]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class OhlcTimeseriesController : ControllerBase
    {
        private readonly ILogger<OhlcTimeseriesController> _logger;
        private readonly IBus _bus;

        public OhlcTimeseriesController(ILogger<OhlcTimeseriesController> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        [HttpPost, Authorize(AuthPolicy.Admin)]
        [SwaggerOperation("[ADMIN] Create OHLC timeseries range")]
        public async Task CreateOhlcTimeseriesRangeAsync([FromBody] OhlcTimeseriesRangeDto request)
        {
            await _bus.Call<CreateOhlcTimeseries, SuccessResponse>(
                new CreateOhlcTimeseries
                {
                    Ohlcs = request
                });
        }

        [HttpGet]
        [ProducesResponseType(typeof(OhlcTimeseriesRangeDto), Status200OK)]
        [SwaggerOperation("Search OHLC timeseries range")]
        public async Task<OhlcTimeseriesRangeDto> GetOhlcTimeseriesRangeAsync(
            [FromQuery] OhlcTimeseriesFilterDto request)
        {
            var payload = await _bus.Call<GetOhlcTimeseries, OhlcTimeseriesResponse>(
                new GetOhlcTimeseries
                {
                    Filter = request
                });

            return payload.Ohlcs;
        }

        [HttpDelete, Authorize(AuthPolicy.Admin)]
        [SwaggerOperation("[ADMIN] Delete OHLC timeseries range")]
        public async Task DeleteOhlcTimeseriesRangeAsync([FromQuery] OhlcTimeseriesFilterDto request)
        {
            await _bus.Call<DeleteOhlcTimeseries, SuccessResponse>(new DeleteOhlcTimeseries
            {
                Filter = request
            });
        }
    }
}