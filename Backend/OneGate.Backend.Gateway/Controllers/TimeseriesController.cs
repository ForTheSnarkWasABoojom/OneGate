using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Contracts.Common;
using OneGate.Backend.Contracts.Timeseries;
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
    public class TimeseriesController : ControllerBase
    {
        private readonly ILogger<TimeseriesController> _logger;
        private readonly IBus _bus;

        public TimeseriesController(ILogger<TimeseriesController> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        [HttpPost, Authorize(AuthPolicy.Admin)]
        [SwaggerOperation("[ADMIN] Create OHLC timeseries range")]
        [Route("ohlc")]
        public async Task CreateOhlcTimeseriesRangeAsync([FromBody] OhlcTimeseriesRangeDto request)
        {
            await _bus.Call<CreateOhlcTimeseriesRange, SuccessResponse>(
                new CreateOhlcTimeseriesRange
                {
                    Ohlcs = request
                });
        }

        [HttpGet]
        [ProducesResponseType(typeof(OhlcTimeseriesRangeDto), Status200OK)]
        [SwaggerOperation("Search OHLC timeseries range")]
        [Route("ohlc")]
        public async Task<OhlcTimeseriesRangeDto> GetOhlcTimeseriesRangeAsync(
            [FromQuery] OhlcTimeseriesFilterDto request)
        {
            var payload = await _bus.Call<GetOhlcTimeseriesRange, OhlcTimeseriesRangeResponse>(
                new GetOhlcTimeseriesRange
                {
                    Filter = request
                });

            return payload.Ohlcs;
        }

        [HttpDelete, Authorize(AuthPolicy.Admin)]
        [SwaggerOperation("[ADMIN] Delete OHLC timeseries range")]
        [Route("ohlc")]
        public async Task DeleteOhlcTimeseriesRangeAsync([FromQuery] OhlcTimeseriesFilterDto request)
        {
            await _bus.Call<DeleteOhlcTimeseriesRange, SuccessResponse>(new DeleteOhlcTimeseriesRange
            {
                Filter = request
            });
        }

        [HttpPost, Authorize(AuthPolicy.Admin)]
        [SwaggerOperation("[ADMIN] Create value timeseries range")]
        [Route("value")]
        public async Task CreateValueTimeseriesRangeAsync([FromBody] ValueTimeseriesRangeDto request)
        {
            await _bus.Call<CreateValueTimeseriesRange, SuccessResponse>(
                new CreateValueTimeseriesRange
                {
                    Values = request
                });
        }

        [HttpGet]
        [ProducesResponseType(typeof(ValueTimeseriesRangeDto), Status200OK)]
        [SwaggerOperation("Search value timeseries range")]
        [Route("value")]
        public async Task<ValueTimeseriesRangeDto> GetValueTimeseriesRangeAsync(
            [FromQuery] ValueTimeseriesFilterDto request)
        {
            var payload = await _bus.Call<GetValueTimeseriesRange, ValueTimeseriesRangeResponse>(
                new GetValueTimeseriesRange
                {
                    Filter = request
                });

            return payload.Values;
        }

        [HttpDelete, Authorize(AuthPolicy.Admin)]
        [SwaggerOperation("[ADMIN] Delete value timeseries range")]
        [Route("value")]
        public async Task DeleteValueTimeseriesRangeAsync([FromQuery] ValueTimeseriesFilterDto request)
        {
            await _bus.Call<DeleteValueTimeseriesRange, SuccessResponse>(new DeleteValueTimeseriesRange
            {
                Filter = request
            });
        }
    }
}