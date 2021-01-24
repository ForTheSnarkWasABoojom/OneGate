using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.AdminApi.Converters;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Series.Point;
using OneGate.Shared.ApiModels.Series.Point;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.AdminApi.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "point_series")]
    public class PointSeriesController : BaseController
    {
        private readonly ILogger<PointSeriesController> _logger;
        private readonly IConverter _converter;
        private readonly IOgBus _bus;

        public PointSeriesController(ILogger<PointSeriesController> logger, IOgBus bus, IConverter converter)
        {
            _logger = logger;
            _bus = bus;
            _converter = converter;
        }

        [HttpPost]
        [SwaggerOperation("Create point timeseries range")]
        public async Task<IActionResult> CreatePointSeriesAsync([FromBody] PointSeriesModel request)
        {
            var createPointSeriesDto = _converter.ToDto(request);
            await _bus.Call<CreatePointSeries, SuccessResponse>(
                new CreatePointSeries
                {
                    Series = createPointSeriesDto
                });

            return Ok();
        }

        [HttpDelete]
        [SwaggerOperation("Delete point timeseries range")]
        public async Task<IActionResult> DeletePointSeriesAsync([FromQuery] PointSeriesFilterModel request)
        {
            var pointSeriesFilterDto = _converter.ToDto(request);
            await _bus.Call<DeletePointSeries, SuccessResponse>(new DeletePointSeries
            {
                Filter = pointSeriesFilterDto
            });

            return Ok();
        }
    }
}