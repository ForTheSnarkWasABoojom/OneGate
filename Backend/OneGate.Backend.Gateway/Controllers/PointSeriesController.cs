using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Contracts.Common;
using OneGate.Backend.Contracts.Series.Point;
using OneGate.Backend.Gateway.Middleware;
using OneGate.Backend.Rpc;
using OneGate.Shared.Models.Common;
using OneGate.Shared.Models.Series.Point;
using Swashbuckle.AspNetCore.Annotations;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace OneGate.Backend.Gateway.Controllers
{
    [ApiController]
    [ProducesResponseType(typeof(ErrorDto), Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorDto), Status400BadRequest)]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PointSeriesController : ControllerBase
    {
        private readonly ILogger<PointSeriesController> _logger;
        private readonly IOgBus _bus;

        public PointSeriesController(ILogger<PointSeriesController> logger, IOgBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        [HttpPost, Authorize(GroupPolicies.Admin)]
        [SwaggerOperation("[ADMIN] Create point timeseries range")]
        public async Task CreatePointSeriesAsync([FromBody] PointSeriesDto request)
        {
            await _bus.Call<CreatePointSeries, SuccessResponse>(
                new CreatePointSeries
                {
                    Series = request
                });
        }

        [HttpGet]
        [ProducesResponseType(typeof(PointSeriesDto), Status200OK)]
        [SwaggerOperation("Search point timeseries range")]
        public async Task<PointSeriesDto> GetPointSeriesAsync(
            [FromQuery] PointSeriesFilterDto request)
        {
            var payload = await _bus.Call<GetPointSeries, PointSeriesResponse>(
                new GetPointSeries
                {
                    Filter = request
                });

            return payload.Series;
        }

        [HttpDelete, Authorize(GroupPolicies.Admin)]
        [SwaggerOperation("[ADMIN] Delete point timeseries range")]
        public async Task DeletePointSeriesAsync([FromQuery] PointSeriesFilterDto request)
        {
            await _bus.Call<DeletePointSeries, SuccessResponse>(new DeletePointSeries
            {
                Filter = request
            });
        }
    }
}