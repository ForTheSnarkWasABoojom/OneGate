﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Transport.Dto.Order;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Gateway.Base.Extensions.Claims;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Order;
using OneGate.Shared.ApiModels.Common;
using OneGate.Shared.ApiModels.Order;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.UserApi.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "orders")]
    public class OrdersController : BaseController
    {
        private readonly ILogger<OrdersController> _logger;

        private readonly IMapper _mapper;
        private readonly IOgBus _bus;

        public OrdersController(ILogger<OrdersController> logger, IOgBus bus, IMapper mapper)
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
            var createOrderDto = _mapper.Map<CreateOrderModel, CreateOrderDto>(request);
            var payload = await _bus.Call<CreateOrder, CreatedResourceResponse>(new CreateOrder
            {
                Order = createOrderDto,
                OwnerId = User.GetAccountId()
            });

            return CreatedAtAction(nameof(GetOrderAsync), new {id = payload.Resource.Id});
        }

        [HttpGet]
        [ActionName(nameof(GetOrderAsync))]
        [ProducesResponseType(typeof(OrderModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation("Order details")]
        [Route("{id}")]
        public async Task<IActionResult> GetOrderAsync([FromRoute] int id)
        {
            var payload = await _bus.Call<GetOrders, OrdersResponse>(new GetOrders
            {
                Filter = new OrderFilterDto
                {
                    Id = id
                },
                OwnerId = User.GetAccountId()
            });

            var response = _mapper.Map<OrderDto, OrderModel>(payload.Orders.FirstOrDefault());
            return StrictOk(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderModel>), StatusCodes.Status200OK)]
        [SwaggerOperation("Search orders")]
        public async Task<IActionResult> GetOrdersRangeAsync([FromQuery] OrderFilterRequest request)
        {
            var orderFilterDto = _mapper.Map<OrderFilterRequest, OrderFilterDto>(request);
            var payload = await _bus.Call<GetOrders, OrdersResponse>(new GetOrders
            {
                Filter = orderFilterDto,
                OwnerId = User.GetAccountId()
            });

            var response = _mapper.Map<IEnumerable<OrderDto>, IEnumerable<OrderModel>>(payload.Orders);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation("Delete existing order")]
        [Route("{id}")]
        public async Task<IActionResult> DeleteOrderAsync([FromRoute] int id)
        {
            var payload = await _bus.Call<DeleteOrder, SuccessResponse>(new DeleteOrder
            {
                Id = id,
                OwnerId = User.GetAccountId()
            });

            return Ok();
        }
    }
}