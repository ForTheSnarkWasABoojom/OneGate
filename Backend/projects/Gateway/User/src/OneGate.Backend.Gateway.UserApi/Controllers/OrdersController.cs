using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Core.Base.Contracts;
using OneGate.Backend.Core.Users.Contracts.Order;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Gateway.Base.Extensions.Claims;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Bus.Contracts;
using OneGate.Shared.ApiModels.User.Order;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.UserApi.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "orders")]
    public class OrdersController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<OrdersController> _logger;
        private readonly ITransportBus _bus;

        public OrdersController(ILogger<OrdersController> logger, ITransportBus bus, IMapper mapper)
        {
            _logger = logger;
            _bus = bus;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation("Create new order")]
        public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderModel request)
        {
            var orderDto = _mapper.Map<CreateOrderModel, OrderDto>(request);
            var payload = await _bus.RequestAsync<CreateOrder, CreatedResourceResponse>(new CreateOrder
            {
                Order = orderDto
            });

            return CreatedAtAction(nameof(GetOrderAsync), new
            {
                id = payload.Id
            });
        }

        [HttpGet]
        [ActionName(nameof(GetOrderAsync))]
        [ProducesResponseType(typeof(OrderModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation("Order details")]
        [Route("{id}")]
        public async Task<IActionResult> GetOrderAsync([FromRoute] int id)
        {
            var payload = await _bus.RequestAsync<GetOrders, OrdersResponse>(new GetOrders
            {
                Id = id,
                OwnerId = User.GetAccountId()
            });
            var orderDto = payload.Orders.FirstOrDefault();

            var order = _mapper.Map<OrderDto, OrderModel>(orderDto);
            return StrictOk(order);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderModel>), StatusCodes.Status200OK)]
        [SwaggerOperation("Get orders by specified filter")]
        public async Task<IActionResult> GetOrdersRangeAsync([FromQuery] OrderFilterModel request)
        {
            var payload = await _bus.RequestAsync<GetOrders, OrdersResponse>(new GetOrders
            {
                Id = request.Id,
                OwnerId = User.GetAccountId()
            });
            var ordersDto = payload.Orders;

            var orders = _mapper.Map<IEnumerable<OrderDto>, IEnumerable<OrderModel>>(ordersDto);
            return Ok(orders);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation("Delete existing order")]
        [Route("{id}")]
        public async Task<IActionResult> DeleteOrderAsync([FromRoute] int id)
        {
            var payload = await _bus.RequestAsync<DeleteOrder, SuccessResponse>(new DeleteOrder
            {
                Id = id,
                OwnerId = User.GetAccountId()
            });

            return Ok();
        }
    }
}