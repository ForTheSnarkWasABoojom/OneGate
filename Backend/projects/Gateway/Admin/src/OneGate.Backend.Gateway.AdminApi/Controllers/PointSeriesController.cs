using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Transport.Dto.Series.Point;
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
        private readonly IOgBus _bus;
        private readonly IMapper _mapper;

        public PointSeriesController(ILogger<PointSeriesController> logger, IOgBus bus, IMapper mapper)
        {
            _logger = logger;
            _bus = bus;
            _mapper = mapper;
        }

        [HttpPost]
        [SwaggerOperation("Create point timeseries range")]
        public async Task<IActionResult> CreatePointSeriesAsync([FromBody] PointSeriesModel request)
        {
            var createPointSeriesDto = _mapper.Map<PointSeriesModel,PointSeriesDto>(request);
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
            var pointSeriesFilterDto = _mapper.Map<PointSeriesFilterModel, PointSeriesFilterDto>(request); 
            await _bus.Call<DeletePointSeries, SuccessResponse>(new DeletePointSeries
            {
                Filter = pointSeriesFilterDto
            });
            
            return Ok();
        }
    }
}