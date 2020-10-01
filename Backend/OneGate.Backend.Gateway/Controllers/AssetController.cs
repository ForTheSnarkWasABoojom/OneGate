using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.Middleware;
using OneGate.Backend.Rpc.Contracts.Asset.CreateAsset;
using OneGate.Backend.Rpc.Contracts.Asset.DeleteAsset;
using OneGate.Backend.Rpc.Contracts.Asset.GetAsset;
using OneGate.Backend.Rpc.Contracts.Asset.GetAssetsByFilter;
using OneGate.Backend.Rpc.Services;
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
        private readonly IAssetService _assetService;

        public AssetController(ILogger<AssetController> logger, IAssetService assetService)
        {
            _logger = logger;
            _assetService = assetService;
        }

        [HttpPost, Authorize(AuthPolicy.Admin)]
        [ProducesResponseType(typeof(CreatedResourceDto), Status200OK)]
        [SwaggerOperation("[ADMIN] Create asset")]
        public async Task<CreatedResourceDto> CreateAssetAsync([FromBody] CreateAssetBaseDto request)
        {
            var payload = await _assetService.CreateAssetAsync(new CreateAssetRequest
            {
                Asset = request
            });

            return payload.CreatedResource;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<AssetBaseDto>), Status200OK)]
        [SwaggerOperation("Search assets")]
        public async Task<List<AssetBaseDto>> GetAssetsByFilterAsync([FromQuery] AssetBaseFilterDto request)
        {
            var payload = await _assetService.GetAssetsByFilterAsync(new GetAssetsByFilterRequest
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
            var payload = await _assetService.GetAssetAsync(new GetAssetRequest
            {
                Id = id
            });

            return payload.Asset;
        }

        [HttpDelete, Authorize(AuthPolicy.Admin)]
        [ProducesResponseType(typeof(AssetBaseDto), Status200OK)]
        [SwaggerOperation("[ADMIN] Delete asset")]
        [Route("{id}")]
        public async Task DeleteAssetAsync([FromRoute] int id)
        {
            var payload = await _assetService.DeleteAssetAsync(new DeleteAssetRequest
            {
                Id = id
            });
        }
    }
}