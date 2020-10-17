using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.Middleware;
using OneGate.Backend.Rpc.Contracts.Timeseries.CreateOhlcTimeseries;
using OneGate.Backend.Rpc.Contracts.Timeseries.CreateValueTimeseries;
using OneGate.Backend.Rpc.Contracts.Timeseries.DeleteOhlcTimeseries;
using OneGate.Backend.Rpc.Contracts.Timeseries.DeleteValueTimeseries;
using OneGate.Backend.Rpc.Contracts.Timeseries.GetOhlcTimeseriesByFilter;
using OneGate.Backend.Rpc.Contracts.Timeseries.GetValueTimeseriesByFilter;
using OneGate.Backend.Rpc.Services;
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
        private readonly ITimeseriesService _timeseriesService;

        public TimeseriesController(ILogger<TimeseriesController> logger, ITimeseriesService timeseriesService)
        {
            _logger = logger;
            _timeseriesService = timeseriesService;
        }

        [HttpPost, Authorize(AuthPolicy.Admin)]
        [ProducesResponseType(typeof(ResourceDto), Status200OK)]
        [SwaggerOperation("[ADMIN] Create OHLC timeseries range")]
        [Route("ohlc")]
        public async Task<ResourceDto> CreateOhlcTimeseriesAsync([FromBody] OhlcTimeseriesRangeDto request)
        {
            var payload = await _timeseriesService.CreateOhlcTimeseriesAsync(new CreateOhlcTimeseriesRequest
            {
                OhlcRange = request
            });

            return payload.Resource;
        }

        [HttpGet]
        [ProducesResponseType(typeof(OhlcTimeseriesRangeDto), Status200OK)]
        [SwaggerOperation("Search OHLC timeseries range")]
        [Route("ohlc")]
        public async Task<OhlcTimeseriesRangeDto> GetOhlcTimeseriesByFilterAsync([FromQuery] OhlcTimeseriesFilterDto request)
        {
            var payload = await _timeseriesService.GetOhlcTimeseriesByFilterAsync(new GetOhlcTimeseriesByFilterRequest
            {
                Filter = request
            });

            return payload.OhlcRange;
        }

        [HttpDelete, Authorize(AuthPolicy.Admin)]
        [SwaggerOperation("[ADMIN] Delete OHLC timeseries range")]
        [Route("ohlc")]
        public async Task DeleteOhlcTimeseriesAsync([FromQuery] OhlcTimeseriesFilterDto request)
        {
            await _timeseriesService.DeleteOhlcTimeseriesAsync(new DeleteOhlcTimeseriesRequest
            {
                Filter = request
            });
        }
        
        [HttpPost, Authorize(AuthPolicy.Admin)]
        [ProducesResponseType(typeof(ResourceDto), Status200OK)]
        [SwaggerOperation("[ADMIN] Create value timeseries range")]
        [Route("value")]
        public async Task<ResourceDto> CreateOhlcTimeseriesAsync([FromBody] CreateValueTimeseriesRangeDto request)
        {
            var payload = await _timeseriesService.CreateValueTimeseriesAsync(new CreateValueTimeseriesRequest
            {
                ValueRange = request
            });

            return payload.Resource;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ValueTimeseriesRangeDto), Status200OK)]
        [SwaggerOperation("Search value timeseries range")]
        [Route("value")]
        public async Task<ValueTimeseriesRangeDto> GetValueTimeseriesByFilterAsync(
            [FromQuery] ValueTimeseriesFilterDto request)
        {
            var payload = await _timeseriesService.GetValueTimeseriesByFilterAsync(new GetValueTimeseriesByFilterRequest
            {
                Filter = request
            });

            return payload.ValueTimeseriesRange;
        }

        [HttpDelete, Authorize(AuthPolicy.Admin)]
        [SwaggerOperation("[ADMIN] Delete value timeseries range")]
        [Route("value")]
        public async Task DeleteValueTimeseriesAsync([FromQuery] ValueTimeseriesFilterDto request)
        {
            await _timeseriesService.DeleteValueTimeseriesAsync(new DeleteValueTimeseriesRequest
            {
                Filter = request
            });
        }
    }
}