using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Series.Point;
using OneGate.Common.Models.Series.Point;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.AdminApi.Controllers
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

        [HttpPost]
        [SwaggerOperation("Create point timeseries range")]
        public async Task CreatePointSeriesAsync([FromBody] PointSeriesDto request)
        {
            await _bus.Call<CreatePointSeries, SuccessResponse>(
                new CreatePointSeries
                {
                    Series = request
                });
        }

        [HttpDelete]
        [SwaggerOperation("Delete point timeseries range")]
        public async Task DeletePointSeriesAsync([FromQuery] PointSeriesFilterDto request)
        {
            await _bus.Call<DeletePointSeries, SuccessResponse>(new DeletePointSeries
            {
                Filter = request
            });
        }
    }
}