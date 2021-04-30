using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Core.Shared.Api;
using OneGate.Backend.Core.Shared.Database.Repository;
using OneGate.Backend.Core.Shared.Linq;
using OneGate.Backend.Core.Users.Api.Contracts.Order;
using OneGate.Backend.Core.Users.Database.Models;
using OneGate.Backend.Core.Users.Database.Repository;

namespace OneGate.Backend.Core.Users.Api.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "orders")]
    public class OrdersController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<OrdersController> _logger;
        private readonly IOrderRepository _orders;

        public OrdersController(ILogger<OrdersController> logger, IMapper mapper, IOrderRepository orders)
        {
            _logger = logger;
            _mapper = mapper;
            _orders = orders;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderDto request)
        {
            var order = _mapper.Map<Order>(request);
            await _orders.AddAsync(order);

            return Ok(order.Id);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync([FromQuery] FilterOrdersDto request)
        {
            Expression<Func<Order, bool>> filter = p => true;
            var limits = new QueryLimits(request.Shift, request.Count);

            filter = filter
                .FilterBy(p => p.Id == request.Id, request.Id)
                .FilterBy(p => p.OwnerId == request.OwnerId, request.OwnerId);

            var orders = await _orders.FilterAsync(filter, limits: limits);

            var ordersDto = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return Ok(ordersDto);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteOrderAsync([FromRoute] int id,
            [FromQuery(Name = "owner_id")] int ownerId)
        {
            await _orders.RemoveAsync(p =>
                p.Id == id &&
                p.OwnerId == ownerId
            );

            return Ok();
        }
    }
}