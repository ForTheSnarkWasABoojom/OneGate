using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Core.Timeseries.Api.Client;
using OneGate.Backend.Core.Timeseries.Api.Contracts.Series;
using OneGate.Backend.Gateway.Shared.Api;
using OneGate.Backend.Gateway.User.Api.Contracts.Series;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.User.Api.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "timeseries")]
    public class TimeseriesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<TimeseriesController> _logger;
        private readonly ITimeseriesApiClient _timeseriesApiClient;

        public TimeseriesController(ILogger<TimeseriesController> logger, IMapper mapper,
            ITimeseriesApiClient timeseriesApiClient)
        {
            _logger = logger;
            _mapper = mapper;
            _timeseriesApiClient = timeseriesApiClient;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SeriesModel>), StatusCodes.Status200OK)]
        [SwaggerOperation("Get timeseries by specified filter")]
        public async Task<IActionResult> GetTimeseriesAsync([FromQuery] FilterSeriesRequest request)
        {
            var filter = _mapper.Map<FilterSeriesRequest, FilterSeriesDto>(request);
            var payload = await _timeseriesApiClient.GetTimeseriesAsync(filter);

            var series = _mapper.Map<IEnumerable<SeriesDto>, IEnumerable<SeriesModel>>(payload);
            return Ok(series);
        }
    }
}