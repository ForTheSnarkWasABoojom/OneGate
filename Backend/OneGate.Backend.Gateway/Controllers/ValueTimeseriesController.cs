using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Contracts.Common;
using OneGate.Backend.Contracts.ValueTimeseries;
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
    public class ValueTimeseriesController : ControllerBase
    {
        private readonly ILogger<ValueTimeseriesController> _logger;
        private readonly IBus _bus;

        public ValueTimeseriesController(ILogger<ValueTimeseriesController> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        [HttpPost, Authorize(AuthPolicy.Admin)]
        [SwaggerOperation("[ADMIN] Create value timeseries range")]
        public async Task CreateValueTimeseriesRangeAsync([FromBody] ValueTimeseriesRangeDto request)
        {
            await _bus.Call<CreateValueTimeseries, SuccessResponse>(
                new CreateValueTimeseries
                {
                    Values = request
                });
        }

        [HttpGet]
        [ProducesResponseType(typeof(ValueTimeseriesRangeDto), Status200OK)]
        [SwaggerOperation("Search value timeseries range")]
        public async Task<ValueTimeseriesRangeDto> GetValueTimeseriesRangeAsync(
            [FromQuery] ValueTimeseriesFilterDto request)
        {
            var payload = await _bus.Call<GetValueTimeseries, ValueTimeseriesResponse>(
                new GetValueTimeseries
                {
                    Filter = request
                });

            return payload.Values;
        }

        [HttpDelete, Authorize(AuthPolicy.Admin)]
        [SwaggerOperation("[ADMIN] Delete value timeseries range")]
        public async Task DeleteValueTimeseriesRangeAsync([FromQuery] ValueTimeseriesFilterDto request)
        {
            await _bus.Call<DeleteValueTimeseries, SuccessResponse>(new DeleteValueTimeseries
            {
                Filter = request
            });
        }
    }
}