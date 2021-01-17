using System.Collections.Generic;
using System.Security.Claims;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.UserApi.Controllers;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Portfolio;
using OneGate.Shared.ApiContracts.Portfolio;
using Ploeh.AutoFixture;
using Xunit;

namespace OneGate.Backend.Gateway.UserApi.Tests.Controllers
{
    public class PortfolioControllerTests
    {
        private readonly Fixture _fixture;

        private readonly IOgBus _bus;
        private readonly ILogger<PortfoliosController> _logger;

        private readonly PortfoliosController _controller;

        public PortfolioControllerTests()
        {
            _fixture = new Fixture();
            _bus = A.Fake<IOgBus>();
            _logger = A.Fake<ILogger<PortfoliosController>>();
            _controller = new PortfoliosController(_logger, _bus);
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
        public async void CreatePortfolioAsync_ShouldTouchCreatePortfolio()
        {
            // Arrange.
            var request = _fixture.Create<CreatePortfolioDto>();

            // Act.
            await _controller.CreatePortfolioAsync(request);

            // Assert.
            A.CallTo(() =>
                    _bus.Call<CreatePortfolio, CreatedResourceResponse>(
                        A<CreatePortfolio>.That.Matches(x => x.Portfolio == request)))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void GetPortfoliosAsync_ShouldTouchGetPortfolios()
        {
            // Arrange.
            var request = _fixture.Create<PortfolioFilterDto>();

            // Act
            await _controller.GetPortfoliosRangeAsync(request);

            // Assert.
            A.CallTo(() => _bus.Call<GetPortfolios, PortfoliosResponse>
                    (A<GetPortfolios>.That.Matches(x => x.Filter == request)))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void GetPortfolioAsync_ShouldTouchGetPortfolio()
        {
            // Arrange.
            A.CallTo(() => _bus.Call<GetPortfolios, PortfoliosResponse>(null)).WithAnyArguments()
                .Returns(new PortfoliosResponse
                {
                    Portfolios = new List<PortfolioDto>
                    {
                        _fixture.Create<PortfolioDto>()
                    }
                });
            var request = _fixture.Create<int>();

            // Act
            await _controller.GetPortfolioAsync(request);

            // Assert.
            A.CallTo(() => _bus.Call<GetPortfolios, PortfoliosResponse>
                    (A<GetPortfolios>.That.Matches(x => x.Filter.Id == request)))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void DeletePortfoliosAsync_ShouldTouchDeletePortfolios()
        {
            // Arrange.
            var request = _fixture.Create<int>();

            // Act
            await _controller.DeletePortfolioAsync(request);

            // Assert.
            A.CallTo(() => _bus.Call<DeletePortfolio, SuccessResponse>
                    (A<DeletePortfolio>.That.Matches(x => x.Id == request)))
                .MustHaveHappenedOnceExactly();
        }
    }
}