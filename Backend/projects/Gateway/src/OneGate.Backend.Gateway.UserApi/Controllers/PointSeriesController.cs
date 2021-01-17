using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Series.Point;
using OneGate.Common.Models.Series.Point;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.UserApi.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "point_series")]
    public class PointSeriesController : BaseController
    {
        private readonly ILogger<PointSeriesController> _logger;
        private readonly IOgBus _bus;

        public PointSeriesController(ILogger<PointSeriesController> logger, IOgBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PointSeriesDto), StatusCodes.Status200OK)]
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
    }
}