using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Core.Timeseries.Contracts;
using OneGate.Backend.Core.Timeseries.Contracts.Series;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Transport.Bus;
using OneGate.Shared.ApiModels.User.Timeseries;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.UserApi.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "timeseries")]
    public class TimeseriesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<TimeseriesController> _logger;
        private readonly ITransportBus _bus;

        public TimeseriesController(ILogger<TimeseriesController> logger, ITransportBus bus, IMapper mapper)
        {
            _logger = logger;
            _bus = bus;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SeriesModel>), StatusCodes.Status200OK)]
        [SwaggerOperation("Get timeseries by specified filter")]
        public async Task<IActionResult> GetTimeseriesAsync([FromQuery] SeriesFilterModel request)
        {
            var payload = await _bus.Call<GetSeries, SeriesResponse>(
                new GetSeries
                {
                    LayoutId = request.LayoutId,
                    StartTimestamp = request.StartTimestamp,
                    EndTimestamp = request.EndTimestamp
                });
            var seriesDto = payload.Series;

            var series = _mapper.Map<IEnumerable<SeriesDto>, IEnumerable<SeriesModel>>(seriesDto);
            return Ok(series);
        }
    }
}