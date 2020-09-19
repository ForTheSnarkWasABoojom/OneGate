using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.Middleware;
using OneGate.Backend.Rpc.Contracts.Ohlc.CreateOhlcs;
using OneGate.Backend.Rpc.Contracts.Ohlc.DeleteOhlcs;
using OneGate.Backend.Rpc.Contracts.Ohlc.GetOhlcsByFilter;
using OneGate.Backend.Rpc.Services;
using OneGate.Shared.Models.Common;
using OneGate.Shared.Models.Ohlc;
using Swashbuckle.AspNetCore.Annotations;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace OneGate.Backend.Gateway.Controllers
{
    [ApiController]
    [ProducesResponseType(typeof(ErrorDto), Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorDto), Status400BadRequest)]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class OhlcController : ControllerBase
    {
        private readonly ILogger<OhlcController> _logger;
        private readonly IOhlcService _ohlcService;

        public OhlcController(ILogger<OhlcController> logger, IOhlcService ohlcService)
        {
            _logger = logger;
            _ohlcService = ohlcService;
        }

        [HttpPost, Authorize(AuthPolicy.Admin)]
        [ProducesResponseType(typeof(OhlcRangeDto), Status200OK)]
        [SwaggerOperation("[ADMIN] Create OHLC range")]
        public async Task<OhlcRangeDto> CreateOhlcsAsync([FromBody] CreateOhlcRangeDto request)
        {
            var payload = await _ohlcService.CreateOhlcsAsync(new CreateOhlcsRequest
            {
                OhlcRange = request
            });

            return payload.OhlcRange;
        }

        [HttpGet]
        [ProducesResponseType(typeof(OhlcRangeDto), Status200OK)]
        [SwaggerOperation("Search OHLC range")]
        public async Task<OhlcRangeDto> GetOhlcsByFilterAsync([FromQuery] OhlcFilterDto request)
        {
            var payload = await _ohlcService.GetOhlcsByFilterAsync(new GetOhlcsByFilterRequest
            {
                Filter = request
            });

            return payload.OhlcRange;
        }

        [HttpDelete, Authorize(AuthPolicy.Admin)]
        [SwaggerOperation("[ADMIN] Delete OHLC range")]
        public async Task DeleteOhlcsAsync([FromQuery] OhlcFilterDto request)
        {
            await _ohlcService.DeleteOhlcsAsync(new DeleteOhlcsRequest
            {
                Filter = request
            });
        }
    }
}