using System.Collections.Generic;
using System.Security.Claims;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.Controllers;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Order;
using OneGate.Common.Models.Order;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Xunit;

namespace OneGate.Backend.Gateway.Tests.Controllers
{
    public class OrderControllerTests
    {
        private readonly Fixture _fixture;

        private readonly IOgBus _bus;
        private readonly ILogger<OrderController> _logger;

        private readonly OrderController _controller;

        public OrderControllerTests()
        {
            _fixture = new Fixture();
            _bus = A.Fake<IOgBus>();
            _logger = A.Fake<ILogger<OrderController>>();
            _controller = new OrderController(_logger, _bus);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "default_user")
            }));
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext {User = user}
            };
        }

        [Fact]
        public async void CreateOrderAsync_ShouldTouchCreateOrder()
        {
            // Arrange.
            _fixture.Customizations.Add(new TypeRelay(typeof(CreateOrderDto), typeof(CreateMarketOrderDto)));
            var request = _fixture.Create<CreateOrderDto>();

            // Act.
            await _controller.CreateOrderAsync(request);

            // Assert.
            A.CallTo(() =>
                    _bus.Call<CreateOrder, CreatedResourceResponse>(
                        A<CreateOrder>.That.Matches(x => x.Order == request)))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void GetOrdersAsync_ShouldTouchGetOrders()
        {
            // Arrange.
            var request = _fixture.Create<OrderFilterDto>();

            // Act
            await _controller.GetOrdersRangeAsync(request);

            // Assert.
            A.CallTo(() => _bus.Call<GetOrders, OrdersResponse>
                    (A<GetOrders>.That.Matches(x => x.Filter == request)))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void GetOrderAsync_ShouldTouchGetOrder()
        {
            // Arrange.
            _fixture.Customizations.Add(new TypeRelay(typeof(OrderDto), typeof(MarketOrderDto)));
            A.CallTo(() => _bus.Call<GetOrders, OrdersResponse>(null)).WithAnyArguments()
                .Returns(new OrdersResponse
                {
                    Orders = new List<OrderDto>
                    {
                        _fixture.Create<OrderDto>()
                    }
                });
            var request = _fixture.Create<int>();

            // Act
            await _controller.GetOrderAsync(request);

            // Assert.
            A.CallTo(() => _bus.Call<GetOrders, OrdersResponse>
                    (A<GetOrders>.That.Matches(x => x.Filter.Id == request)))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void DeleteOrdersAsync_ShouldTouchDeleteOrders()
        {
            // Arrange.
            var request = _fixture.Create<int>();

            // Act
            await _controller.DeleteOrderAsync(request);

            // Assert.
            A.CallTo(() => _bus.Call<DeleteOrder, SuccessResponse>
                    (A<DeleteOrder>.That.Matches(x => x.Id == request)))
                .MustHaveHappenedOnceExactly();
        }
    }
}