using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Contracts.Common;
using OneGate.Backend.Contracts.Layout;
using OneGate.Backend.Gateway.Middleware;
using OneGate.Backend.Rpc;
using OneGate.Shared.Models.Common;
using OneGate.Shared.Models.Layout;
using Swashbuckle.AspNetCore.Annotations;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace OneGate.Backend.Gateway.Controllers
{
    [ApiController]
    [ProducesResponseType(typeof(ErrorDto), Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorDto), Status400BadRequest)]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LayoutController: ControllerBase
    {
        private readonly ILogger<LayoutController> _logger;
        private readonly IBus _bus;

        public LayoutController(ILogger<LayoutController> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }
        
        [HttpPost, Authorize(AuthPolicy.Admin)]
        [ProducesResponseType(typeof(ResourceDto), Status200OK)]
        [SwaggerOperation("[ADMIN] Create Layout")]
        public async Task<ResourceDto> CreateLayoutAsync([FromBody] CreateLayoutDto request)
        {
            var payload = await _bus.Call<CreateLayout, CreatedResourceResponse>(new CreateLayout
            {
                Layout = request
            });

            return payload.Resource;
        }

        [HttpGet, Authorize(AuthPolicy.Admin)]
        [ProducesResponseType(typeof(IEnumerable<LayoutDto>), Status200OK)]
        [SwaggerOperation("[ADMIN] Search layouts")]
        public async Task<IEnumerable<LayoutDto>> GetLayoutsRangeAsync([FromQuery] LayoutFilterDto request)
        {
            var payload = await _bus.Call<GetLayouts, LayoutsResponse>(new GetLayouts
            {
                Filter = request
            });

            return payload.Layouts;
        }

        [HttpGet, Authorize(AuthPolicy.Admin)]
        [ProducesResponseType(typeof(LayoutDto), Status200OK)]
        [SwaggerOperation("[ADMIN] Layout details")]
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

        [HttpDelete, Authorize(AuthPolicy.Admin)]
        [SwaggerOperation("[ADMIN] Delete layout")]
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