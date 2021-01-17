using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Asset;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Shared.ApiContracts.Asset;
using OneGate.Shared.ApiContracts.Common;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.AdminApi.Controllers
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
        
        [HttpPost]
        [ProducesResponseType(typeof(ResourceDto), StatusCodes.Status200OK)]
        [SwaggerOperation("Create asset")]
        public async Task<ResourceDto> CreateAssetAsync([FromBody] CreateAssetDto request)
        {
            var payload = await _bus.Call<CreateAsset, CreatedResourceResponse>(new CreateAsset
            {
                Asset = request
            });

            return payload.Resource;
        }
        
        [HttpDelete]
        [SwaggerOperation("Delete asset")]
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