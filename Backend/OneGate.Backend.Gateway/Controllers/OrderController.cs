using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.Extensions;
using OneGate.Backend.Rpc.Contracts.Order.CreateOrder;
using OneGate.Backend.Rpc.Contracts.Order.DeleteOrder;
using OneGate.Backend.Rpc.Contracts.Order.GetOrder;
using OneGate.Backend.Rpc.Contracts.Order.GetOrdersByFilter;
using OneGate.Backend.Rpc.Services;
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
        private readonly IAccountService _accountService;

        public OrderController(ILogger<OrderController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderBaseDto), Status200OK)]
        [SwaggerOperation("Create order")]
        public async Task<CreatedResourceDto> CreateAssetAsync([FromBody] CreateOrderBaseDto request)
        {
            var payload = await _accountService.CreateOrderAsync(new CreateOrderRequest
            {
                Order = request,
                AccountId = User.GetAccountId()
            });

            return payload.CreatedResource;
        }

        [HttpGet]
        [ProducesResponseType(typeof(OrderBaseDto), Status200OK)]
        [SwaggerOperation("Order details")]
        [Route("{id}")]
        public async Task<OrderBaseDto> GetOrderAsync([FromRoute] int id)
        {
            var payload = await _accountService.GetOrderAsync(new GetOrderRequest
            {
                Id = id,
                AccountId = User.GetAccountId()
            });

            return payload.Order;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<OrderBaseDto>), Status200OK)]
        [SwaggerOperation("Search orders")]
        public async Task<List<OrderBaseDto>> GetOrdersByFilterAsync([FromQuery] OrderBaseFilterDto request)
        {
            var payload = await _accountService.GetOrdersByFiltersAsync(new GetOrdersByFilterRequest
            {
                Filter = request,
                AccountId = User.GetAccountId()
            });

            return payload.Orders;
        }

        [HttpDelete]
        [ProducesResponseType(typeof(OrderBaseDto), Status200OK)]
        [SwaggerOperation("Delete order")]
        [Route("{id}")]
        public async Task DeleteAssetAsync([FromRoute] int id)
        {
            var payload = await _accountService.DeleteOrderAsync(new DeleteOrderRequest
            {
                Id = id,
                AccountId = User.GetAccountId()
            });
        }
    }
}