using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        private readonly IMapper _mapper;
        private readonly IOgBus _bus;

        public LayoutsController(ILogger<LayoutsController> logger, IOgBus bus, IMapper mapper)
        {
            _logger = logger;
            _bus = bus;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResourceModel), StatusCodes.Status200OK)]
        [SwaggerOperation("Create Layout")]
        public async Task<IActionResult> CreateLayoutAsync([FromBody] CreateLayoutModel request)
        {
            var createLayoutDto = _mapper.Map<CreateLayoutModel, CreateLayoutDto>(request);
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
            var layoutFilterDto = _mapper.Map<LayoutFilterModel,LayoutFilterDto>(request);
            var payload = await _bus.Call<GetLayouts, LayoutsResponse>(new GetLayouts
            {
                Filter = layoutFilterDto
            });

            var response = _mapper.Map<IEnumerable<LayoutDto>,IEnumerable<LayoutModel>>(payload.Layouts);
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

            var response = _mapper.Map<LayoutDto,LayoutModel>(payload.Layouts.FirstOrDefault());
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