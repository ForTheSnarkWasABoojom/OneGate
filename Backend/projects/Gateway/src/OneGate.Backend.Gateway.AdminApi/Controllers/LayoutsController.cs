using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Layout;
using OneGate.Shared.ApiContracts.Common;
using OneGate.Shared.ApiContracts.Layout;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.AdminApi.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "layouts")]
    public class LayoutsController : BaseController
    {
        private readonly ILogger<LayoutsController> _logger;
        private readonly IOgBus _bus;

        public LayoutsController(ILogger<LayoutsController> logger, IOgBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResourceDto), StatusCodes.Status200OK)]
        [SwaggerOperation("Create Layout")]
        public async Task<ResourceDto> CreateLayoutAsync([FromBody] CreateLayoutDto request)
        {
            var payload = await _bus.Call<CreateLayout, CreatedResourceResponse>(new CreateLayout
            {
                Layout = request
            });

            return payload.Resource;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LayoutDto>), StatusCodes.Status200OK)]
        [SwaggerOperation("Search layouts")]
        public async Task<IEnumerable<LayoutDto>> GetLayoutsRangeAsync([FromQuery] LayoutFilterDto request)
        {
            var payload = await _bus.Call<GetLayouts, LayoutsResponse>(new GetLayouts
            {
                Filter = request
            });

            return payload.Layouts;
        }

        [HttpGet]
        [ProducesResponseType(typeof(LayoutDto), StatusCodes.Status200OK)]
        [SwaggerOperation("Layout details")]
        [Route("{id}")]
        public async Task<LayoutDto> GetLayoutAsync([FromRoute] int id)
        {
            var payload = await _bus.Call<GetLayouts, LayoutsResponse>(new GetLayouts
            {
                Filter = new LayoutFilterDto
                {
                    Id = id
                }
            });

            return payload.Layouts.First();
        }
        
        [HttpDelete]
        [SwaggerOperation("Delete layout")]
        [Route("{id}")]
        public async Task DeleteLayoutAsync([FromRoute] int id)
        {
            await _bus.Call<DeleteLayout, SuccessResponse>(new DeleteLayout
            {
                Id = id
            });
        }
    }
}