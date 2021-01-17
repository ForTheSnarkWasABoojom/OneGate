using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Asset;
using OneGate.Common.Models.Asset;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.UserApi.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "assets")]
    public class AssetsController : BaseController
    {
        private readonly ILogger<AssetsController> _logger;
        private readonly IOgBus _bus;

        public AssetsController(ILogger<AssetsController> logger, IOgBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AssetDto>), StatusCodes.Status200OK)]
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
        [ProducesResponseType(typeof(AssetDto), StatusCodes.Status200OK)]
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
    }
}