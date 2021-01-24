using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Gateway.UserApi.Converters;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Timeseries.Point;
using OneGate.Shared.ApiModels.Timeseries.Point;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.UserApi.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "point_series")]
    public class PointSeriesController : BaseController
    {
        private readonly ILogger<PointSeriesController> _logger;
        
        private readonly IConverter _converter;
        private readonly IOgBus _bus;

        public PointSeriesController(ILogger<PointSeriesController> logger, IOgBus bus,IConverter converter)
        {
            _logger = logger;
            _bus = bus;
            _converter = converter;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PointSeriesModel), StatusCodes.Status200OK)]
        [SwaggerOperation("Get point series by specified filter")]
        public async Task<IActionResult> GetPointSeriesAsync([FromQuery] PointSeriesFilterModel request)
        {
            var pointSeriesFilterDto = _converter.ToDto(request);
            var payload = await _bus.Call<GetPointSeries, PointSeriesResponse>(
                new GetPointSeries
                {
                    Filter = pointSeriesFilterDto
                });

            var response = _converter.FromDto(payload.Series);
            return Ok(response);
        }
    }
}