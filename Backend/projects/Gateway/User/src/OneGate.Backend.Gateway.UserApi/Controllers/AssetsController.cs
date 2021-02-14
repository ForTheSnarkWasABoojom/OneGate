using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Core.Assets.Contracts.Asset;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Transport.Bus;
using OneGate.Shared.ApiModels.User.Asset;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.UserApi.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "assets")]
    public class AssetsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AssetsController> _logger;
        private readonly ITransportBus _bus;

        public AssetsController(ILogger<AssetsController> logger, ITransportBus bus, IMapper mapper)
        {
            _logger = logger;
            _bus = bus;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AssetModel>), StatusCodes.Status200OK)]
        [SwaggerOperation("Get assets by specified filter")]
        public async Task<IActionResult> GetAssetsRangeAsync([FromQuery] AssetFilterModel request)
        {
            var payload = await _bus.Call<GetAssets, AssetsResponse>(new GetAssets
            {
                Id = request.Id,
                Shift = request.Shift,
                Count = request.Count
            });
            var assetsDto = payload.Assets;

            var assets = _mapper.Map<IEnumerable<AssetDto>, IEnumerable<AssetModel>>(assetsDto);
            return Ok(assets);
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
                Id = id
            });
            var assetDto = payload.Assets.FirstOrDefault();

            var assets = _mapper.Map<AssetDto, AssetModel>(assetDto);
            return StrictOk(assets);
        }
    }
}