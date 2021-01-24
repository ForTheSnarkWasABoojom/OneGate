using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Transport.Dto.Asset;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Gateway.UserApi.Converters;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Asset;
using OneGate.Shared.ApiModels.Asset;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.UserApi.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "assets")]
    public class AssetsController : BaseController
    {
        private readonly ILogger<AssetsController> _logger;
        private readonly IConverter _converter;
        private readonly IOgBus _bus;

        public AssetsController(ILogger<AssetsController> logger, IOgBus bus, IConverter converter)
        {
            _logger = logger;
            _bus = bus;
            _converter = converter;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AssetModel>), StatusCodes.Status200OK)]
        [SwaggerOperation("Get assets by specified filter")]
        public async Task<IActionResult> GetAssetsRangeAsync([FromQuery] AssetFilterModel request)
        {
            var assetFilterDto = _converter.ToDto(request);
            var payload = await _bus.Call<GetAssets, AssetsResponse>(new GetAssets
            {
                Filter = assetFilterDto
            });

            var response = payload.Assets.Select(_converter.FromDto);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(AssetModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation("Asset details")]
        [Route("{id}")]
        public async Task<IActionResult> GetAssetAsync([FromRoute] int id)
        {
            var payload = await _bus.Call<GetAssets, AssetsResponse>(new GetAssets
            {
                Filter = new AssetFilterDto
                {
                    Id = id
                }
            });

            var response = _converter.FromDto(payload.Assets.FirstOrDefault());
            return StrictOk(response);
        }
    }
}