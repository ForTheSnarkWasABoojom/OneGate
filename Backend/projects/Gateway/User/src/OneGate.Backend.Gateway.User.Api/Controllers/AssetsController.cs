using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Core.Assets.Api.Client;
using OneGate.Backend.Core.Assets.Api.Contracts.Asset;
using OneGate.Backend.Gateway.Shared;
using OneGate.Shared.ApiModels.User.Asset;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.User.Api.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "assets")]
    public class AssetsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AssetsController> _logger;
        private readonly IAssetsApiClient _assetsApiClient;

        public AssetsController(ILogger<AssetsController> logger, IMapper mapper, IAssetsApiClient assetsApiClient)
        {
            _logger = logger;
            _mapper = mapper;
            _assetsApiClient = assetsApiClient;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Asset>), StatusCodes.Status200OK)]
        [SwaggerOperation("Get assets by specified filter")]
        public async Task<IActionResult> GetAssetsRangeAsync([FromQuery] FilterAssetsRequest request)
        {
            var filter = _mapper.Map<FilterAssetsRequest, FilterAssetsDto>(request);
            var payload = await _assetsApiClient.GetAssetsAsync(filter);

            var assets = _mapper.Map<IEnumerable<AssetDto>, IEnumerable<Asset>>(payload);
            return Ok(assets);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Asset), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation("Asset details")]
        [Route("{id}")]
        public async Task<IActionResult> GetAssetAsync([FromRoute] int id)
        {
            var payload = await _assetsApiClient.GetAssetsAsync(new FilterAssetsDto
            {
                Id = id
            });
            var assetDto = payload.FirstOrDefault();

            var assets = _mapper.Map<AssetDto, Asset>(assetDto);
            return StrictOk(assets);
        }
    }
}