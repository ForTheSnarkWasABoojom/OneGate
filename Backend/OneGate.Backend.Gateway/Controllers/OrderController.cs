using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Contracts.Common;
using OneGate.Backend.Gateway.Extensions;
using OneGate.Backend.Contracts.Order;
using OneGate.Backend.Rpc;
using OneGate.Shared.Models.Common;
using OneGate.Shared.Models.Order;
using Swashbuckle.AspNetCore.Annotations;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace OneGate.Backend.Gateway.Controllers
{
    [ApiController]
    [ProducesResponseType(typeof(ErrorDto), Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorDto), Status400BadRequest)]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOgBus _bus;

        public OrderController(ILogger<OrderController> logger, IOgBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResourceDto), Status200OK)]
        [SwaggerOperation("Create order")]
        public async Task<ResourceDto> CreateOrderAsync([FromBody] CreateOrderDto request)
        {
            var payload = await _bus.Call<CreateOrder, CreatedResourceResponse>(new CreateOrder
            {
                Order = request,
                OwnerId = User.GetAccountId()
            });

            return payload.Resource;
        }

        [HttpGet]
        [ProducesResponseType(typeof(OrderDto), Status200OK)]
        [SwaggerOperation("Order details")]
        [Route("{id}")]
        public async Task<OrderDto> GetOrderAsync([FromRoute] int id)
        {
            var payload = await _bus.Call<GetOrders, OrdersResponse>(new GetOrders
            {
                Filter = new OrderFilterDto
                {
                    Id = id
                },
                OwnerId = User.GetAccountId()
            });

            return payload.Orders.First();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), Status200OK)]
        [SwaggerOperation("Search orders")]
        public async Task<IEnumerable<OrderDto>> GetOrdersRangeAsync([FromQuery] OrderFilterDto request)
        {
            var payload = await _bus.Call<GetOrders, OrdersResponse>(new GetOrders
            {
                Filter = request,
                OwnerId = User.GetAccountId()
            });

            return payload.Orders;
        }

        [HttpDelete]
        [SwaggerOperation("Delete order")]
        [Route("{id}")]
        public async Task DeleteOrderAsync([FromRoute] int id)
        {
            var payload = await _bus.Call<DeleteOrder, SuccessResponse>(new DeleteOrder
            {
                Id = id,
                OwnerId = User.GetAccountId()
            });
        }
    }
}