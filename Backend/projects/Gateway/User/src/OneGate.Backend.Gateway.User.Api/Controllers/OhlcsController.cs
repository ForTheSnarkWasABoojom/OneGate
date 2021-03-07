using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Core.Timeseries.Api.Client;
using OneGate.Backend.Core.Timeseries.Api.Contracts.Ohlc;
using OneGate.Backend.Gateway.Shared.Api;
using OneGate.Backend.Gateway.User.Api.Contracts.Timeseries;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.User.Api.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "ohlc")]
    public class OhlcsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<OhlcsController> _logger;
        private readonly ITimeseriesApiClient _timeseriesApiClient;

        public OhlcsController(ILogger<OhlcsController> logger, IMapper mapper, ITimeseriesApiClient timeseriesApiClient)
        {
            _logger = logger;
            _mapper = mapper;
            _timeseriesApiClient = timeseriesApiClient;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OhlcModel>), StatusCodes.Status200OK)]
        [SwaggerOperation("Get ohlcs by specified filter")]
        public async Task<IActionResult> GetOhlcsAsync([FromQuery] FilterOhlcRequest request)
        {
            var filter = _mapper.Map<FilterOhlcRequest, FilterOhlcDto>(request);
            var payload = await _timeseriesApiClient.GetOhlcsAsync(filter);

            var ohlcs = _mapper.Map<IEnumerable<OhlcDto>, IEnumerable<OhlcModel>>(payload);
            return Ok(ohlcs);
        }
    }
}