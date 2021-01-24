using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.AdminApi.Converters;
using OneGate.Backend.Transport.Dto.Layout;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Layout;
using OneGate.Shared.ApiModels.Common;
using OneGate.Shared.ApiModels.Layout;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.AdminApi.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "layouts")]
    public class LayoutsController : BaseController
    {
        private readonly ILogger<LayoutsController> _logger;
        private readonly IConverter _converter;
        private readonly IOgBus _bus;

        public LayoutsController(ILogger<LayoutsController> logger, IOgBus bus, IConverter converter)
        {
            _logger = logger;
            _bus = bus;
            _converter = converter;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResourceModel), StatusCodes.Status200OK)]
        [SwaggerOperation("Create Layout")]
        public async Task<IActionResult> CreateLayoutAsync([FromBody] CreateLayoutModel request)
        {
            var createLayoutDto = _converter.ToDto(request);
            var payload = await _bus.Call<CreateLayout, CreatedResourceResponse>(new CreateLayout
            {
                Layout = createLayoutDto
            });

            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LayoutDto>), StatusCodes.Status200OK)]
        [SwaggerOperation("Search layouts")]
        public async Task<IActionResult> GetLayoutsRangeAsync([FromQuery] LayoutFilterModel request)
        {
            var layoutFilterDto = _converter.ToDto(request);
            var payload = await _bus.Call<GetLayouts, LayoutsResponse>(new GetLayouts
            {
                Filter = layoutFilterDto
            });

            var response = payload.Layouts.Select(_converter.FromDto);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(LayoutModel), StatusCodes.Status200OK)]
        [SwaggerOperation("Layout details")]
        [Route("{id}")]
        public async Task<IActionResult> GetLayoutAsync([FromRoute] int id)
        {
            var payload = await _bus.Call<GetLayouts, LayoutsResponse>(new GetLayouts
            {
                Filter = new LayoutFilterDto
                {
                    Id = id
                }
            });

            var response = _converter.FromDto(payload.Layouts.FirstOrDefault());
            return StrictOk(response);
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