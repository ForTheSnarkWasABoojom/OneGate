using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Transport.Dto.Series.Point;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Series.Point;
using OneGate.Shared.ApiModels.Series.Point;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.UserApi.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "point_series")]
    public class PointSeriesController : BaseController
    {
        private readonly ILogger<PointSeriesController> _logger;
        
        private readonly IMapper _mapper;
        private readonly IOgBus _bus;

        public PointSeriesController(ILogger<PointSeriesController> logger, IOgBus bus, IMapper mapper)
        {
            _logger = logger;
            _bus = bus;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PointSeriesModel), StatusCodes.Status200OK)]
        [SwaggerOperation("Get point timeseries by specified filter")]
        public async Task<IActionResult> GetPointSeriesAsync([FromQuery] PointSeriesFilterModel request)
        {
            var pointSeriesFilterDto = _mapper.Map<PointSeriesFilterModel, PointSeriesFilterDto>(request);
            var payload = await _bus.Call<GetPointSeries, PointSeriesResponse>(
                new GetPointSeries
                {
                    Filter = pointSeriesFilterDto
                });

            var response = _mapper.Map<PointSeriesDto, PointSeriesModel>(payload.Series);
            return Ok(response);
        }
    }
}