using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OneGate.Backend.Gateway.Middleware;
using OneGate.Backend.Contracts.Asset;
using OneGate.Backend.Contracts.Common;
using OneGate.Backend.Rpc;
using OneGate.Shared.Models.Asset;
using OneGate.Shared.Models.Common;
using Swashbuckle.AspNetCore.Annotations;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace OneGate.Backend.Gateway.Controllers
{
    [ApiController]
    [ProducesResponseType(typeof(ErrorDto), Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorDto), Status400BadRequest)]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AssetController : ControllerBase
    {
        private readonly ILogger<AssetController> _logger;
        private readonly IBus _bus;

        public AssetController(ILogger<AssetController> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        [HttpPost, Authorize(AuthPolicy.Admin)]
        [ProducesResponseType(typeof(ResourceDto), Status200OK)]
        [SwaggerOperation("[ADMIN] Create asset")]
        public async Task<ResourceDto> CreateAssetAsync([FromBody] CreateAssetBaseDto request)
        {
            var payload = await _bus.Call<CreateAsset,CreatedResourceResponse>(new CreateAsset
            {
                Asset = request
            });

            return payload.Resource;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AssetBaseDto>), Status200OK)]
        [SwaggerOperation("Search assets")]
        public async Task<IEnumerable<AssetBaseDto>> GetAssetsRangeAsync([FromQuery] AssetBaseFilterDto request)
        {
            var payload = await _bus.Call<GetAssetsRange,AssetsRangeResponse>(new GetAssetsRange
            {
                Filter = request
            });

            return payload.Assets;
        }

        [HttpGet]
        [ProducesResponseType(typeof(AssetBaseDto), Status200OK)]
        [SwaggerOperation("Asset details")]
        [Route("{id}")]
        public async Task<AssetBaseDto> GetAssetAsync([FromRoute] int id)
        {
            var payload = await _bus.Call<GetAsset,AssetResponse>(new GetAsset
            {
                Id = id
            });

            return payload.Asset;
        }

        [HttpDelete, Authorize(AuthPolicy.Admin)]
        [SwaggerOperation("[ADMIN] Delete asset")]
        [Route("{id}")]
        public async Task DeleteAssetAsync([FromRoute] int id)
        {
            await _bus.Call<DeleteAsset,SuccessResponse>(new DeleteAsset
            {
                Id = id
            });
        }
    }
}