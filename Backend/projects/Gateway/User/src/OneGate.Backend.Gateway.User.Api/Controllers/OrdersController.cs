using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Core.Users.Api.Client;
using OneGate.Backend.Core.Users.Api.Contracts.Order;
using OneGate.Backend.Gateway.Shared;
using OneGate.Backend.Gateway.Shared.Extensions.Claims;
using OneGate.Shared.ApiModels.User.Order;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.User.Api.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "orders")]
    public class OrdersController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<OrdersController> _logger;
        private readonly IUsersApiClient _usersApiClient;

        public OrdersController(ILogger<OrdersController> logger, IMapper mapper, IUsersApiClient usersApiClient)
        {
            _logger = logger;
            _mapper = mapper;
            _usersApiClient = usersApiClient;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation("Create new order")]
        public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderRequest request)
        {
            var orderDto = _mapper.Map<CreateOrderRequest, OrderDto>(request);
            var payload = await _usersApiClient.CreateOrderAsync(orderDto);

            return CreatedAtAction(nameof(GetOrderAsync), new
            {
                id = payload
            });
        }

        [HttpGet]
        [ActionName(nameof(GetOrderAsync))]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation("Order details")]
        [Route("{id}")]
        public async Task<IActionResult> GetOrderAsync([FromRoute] int id)
        {
            var payload = await _usersApiClient.GetOrdersAsync(new FilterOrdersDto
            {
                Id = id,
                OwnerId = User.GetAccountId()
            });
            var orderDto = payload.FirstOrDefault();

            var order = _mapper.Map<OrderDto, Order>(orderDto);
            return StrictOk(order);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Order>), StatusCodes.Status200OK)]
        [SwaggerOperation("Get orders by specified filter")]
        public async Task<IActionResult> GetOrdersRangeAsync([FromQuery] FilterOrdersRequest request)
        {
            var filter = _mapper.Map<FilterOrdersRequest, FilterOrdersDto>(request);
            filter.OwnerId = User.GetAccountId();
            var payload = await _usersApiClient.GetOrdersAsync(filter);

            var orders = _mapper.Map<IEnumerable<OrderDto>, IEnumerable<Order>>(payload);
            return Ok(orders);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation("Delete existing order")]
        [Route("{id}")]
        public async Task<IActionResult> DeleteOrderAsync([FromRoute] int id)
        {
            await _usersApiClient.DeleteOrderAsync(id, User.GetAccountId());

            return Ok();
        }
    }
}