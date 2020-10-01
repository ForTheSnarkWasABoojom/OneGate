using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.Middleware;
using OneGate.Backend.Rpc.Contracts.Exchange.CreateExchange;
using OneGate.Backend.Rpc.Contracts.Exchange.DeleteExchange;
using OneGate.Backend.Rpc.Contracts.Exchange.GetExchange;
using OneGate.Backend.Rpc.Contracts.Exchange.GetExchangesByFilter;
using OneGate.Backend.Rpc.Services;
using OneGate.Shared.Models.Common;
using OneGate.Shared.Models.Exchange;
using Swashbuckle.AspNetCore.Annotations;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace OneGate.Backend.Gateway.Controllers
{
    [ApiController]
    [ProducesResponseType(typeof(ErrorDto), Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorDto), Status400BadRequest)]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ExchangeController : ControllerBase
    {
        private readonly ILogger<ExchangeController> _logger;
        private readonly IAssetService _assetService;

        public ExchangeController(ILogger<ExchangeController> logger, IAssetService assetService)
        {
            _logger = logger;
            _assetService = assetService;
        }

        [HttpPost, Authorize(AuthPolicy.Admin)]
        [ProducesResponseType(typeof(CreatedResourceDto), Status200OK)]
        [SwaggerOperation("[ADMIN] Create exchange")]
        public async Task<CreatedResourceDto> CreateExchangeAsync([FromBody] CreateExchangeDto request)
        {
            var payload = await _assetService.CreateExchangeAsync(new CreateExchangeRequest
            {
                Exchange = request
            });

            return payload.CreatedResource;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ExchangeDto>), Status200OK)]
        [SwaggerOperation("Search exchanges")]
        public async Task<List<ExchangeDto>> GetExchangesByFilterAsync([FromQuery] ExchangeFilterDto request)
        {
            var payload = await _assetService.GetExchangesByFilterAsync(new GetExchangesByFilterRequest
            {
                Filter = request
            });

            return payload.Exchanges;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ExchangeDto), Status200OK)]
        [SwaggerOperation("Exchange details")]
        [Route("{id}")]
        public async Task<ExchangeDto> GetExchangeAsync([FromRoute] int id)
        {
            var payload = await _assetService.GetExchangeAsync(new GetExchangeRequest
            {
                Id = id
            });

            return payload.Exchange;
        }

        [HttpDelete, Authorize(AuthPolicy.Admin)]
        [ProducesResponseType(typeof(ExchangeDto), Status200OK)]
        [SwaggerOperation("[ADMIN] Delete exchange")]
        [Route("{id}")]
        public async Task DeleteExchangeAsync([FromRoute] int id)
        {
            var payload = await _assetService.DeleteExchangeAsync(new DeleteExchangeRequest
            {
                Id = id
            });
        }
    }
}