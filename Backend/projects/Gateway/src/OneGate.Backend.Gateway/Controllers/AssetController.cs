using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.Middleware;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Asset;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Common.Models.Asset;
using OneGate.Common.Models.Common;
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
        private readonly IOgBus _bus;

        public AssetController(ILogger<AssetController> logger, IOgBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        [HttpPost, Authorize(GroupPolicies.Admin)]
        [ProducesResponseType(typeof(ResourceDto), Status200OK)]
        [SwaggerOperation("[ADMIN] Create asset")]
        public async Task<ResourceDto> CreateAssetAsync([FromBody] CreateAssetDto request)
        {
            var payload = await _bus.Call<CreateAsset, CreatedResourceResponse>(new CreateAsset
            {
                Asset = request
            });

            return payload.Resource;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AssetDto>), Status200OK)]
        [SwaggerOperation("Search assets")]
        public async Task<IEnumerable<AssetDto>> GetAssetsRangeAsync([FromQuery] AssetFilterDto request)
        {
            var payload = await _bus.Call<GetAssets, AssetsResponse>(new GetAssets
            {
                Filter = request
            });

            return payload.Assets;
        }

        [HttpGet]
        [ProducesResponseType(typeof(AssetDto), Status200OK)]
        [SwaggerOperation("Asset details")]
        [Route("{id}")]
        public async Task<AssetDto> GetAssetAsync([FromRoute] int id)
        {
            var payload = await _bus.Call<GetAssets, AssetsResponse>(new GetAssets
            {
                Filter = new AssetFilterDto
                {
                    Id = id
                }
            });

            return payload.Assets.First();
        }

        [HttpDelete, Authorize(GroupPolicies.Admin)]
        [SwaggerOperation("[ADMIN] Delete asset")]
        [Route("{id}")]
        public async Task DeleteAssetAsync([FromRoute] int id)
        {
            await _bus.Call<DeleteAsset, SuccessResponse>(new DeleteAsset
            {
                Id = id
            });
        }
    }
}