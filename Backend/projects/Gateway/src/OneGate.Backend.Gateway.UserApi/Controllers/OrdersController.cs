using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Gateway.Base.Extensions.Claims;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Order;
using OneGate.Shared.ApiContracts.Common;
using OneGate.Shared.ApiContracts.Order;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.UserApi.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "orders")]
    public class OrdersController : BaseController
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IOgBus _bus;

        public OrdersController(ILogger<OrdersController> logger, IOgBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResourceDto), StatusCodes.Status200OK)]
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
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
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
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), StatusCodes.Status200OK)]
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