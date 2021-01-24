using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.AdminApi.Converters;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Asset;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Shared.ApiModels.Asset;
using OneGate.Shared.ApiModels.Common;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.AdminApi.Controllers
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

        [HttpPost]
        [ProducesResponseType(typeof(ResourceModel), StatusCodes.Status200OK)]
        [SwaggerOperation("Create asset")]
        public async Task<IActionResult> CreateAssetAsync([FromBody] CreateAssetModel request)
        {
            var createdAccountDto = _converter.ToDto(request);
            var payload = await _bus.Call<CreateAsset, CreatedResourceResponse>(new CreateAsset
            {
                Asset = createdAccountDto
            });

            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation("Delete asset")]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAssetAsync([FromRoute] int id)
        {
            var payload = await _bus.Call<DeleteAsset, SuccessResponse>(new DeleteAsset
            {
                Id = id
            });

            return Ok();
        }
    }
}