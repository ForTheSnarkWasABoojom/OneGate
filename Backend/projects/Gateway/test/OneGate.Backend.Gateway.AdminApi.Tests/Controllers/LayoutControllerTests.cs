using System.Collections.Generic;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.AdminApi.Controllers;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Layout;
using OneGate.Shared.ApiContracts.Layout;
using Ploeh.AutoFixture;
using Xunit;

namespace OneGate.Backend.Gateway.AdminApi.Tests.Controllers
{
    public class LayoutControllerTests
    {
        private readonly Fixture _fixture;

        private readonly IOgBus _bus;
        private readonly ILogger<LayoutsController> _logger;

        private readonly LayoutsController _controller;

        public LayoutControllerTests()
        {
            _fixture = new Fixture();
            _bus = A.Fake<IOgBus>();
            _logger = A.Fake<ILogger<LayoutsController>>();
            _controller = new LayoutsController(_logger, _bus);
        }

        [Fact]
        public async void CreateLayoutAsync_ShouldTouchCreateLayout()
        {
            // Arrange.
            var request = _fixture.Create<CreateLayoutDto>();

            // Act.
            await _controller.CreateLayoutAsync(request);

            // Assert.
            A.CallTo(() =>
                    _bus.Call<CreateLayout, CreatedResourceResponse>(
                        A<CreateLayout>.That.Matches(x => x.Layout == request)))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void GetLayoutsAsync_ShouldTouchGetLayouts()
        {
            // Arrange.
            var request = _fixture.Create<LayoutFilterDto>();

            // Act
            await _controller.GetLayoutsRangeAsync(request);

            // Assert.
            A.CallTo(() => _bus.Call<GetLayouts, LayoutsResponse>
                    (A<GetLayouts>.That.Matches(x => x.Filter == request)))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void GetLayoutAsync_ShouldTouchGetLayout()
        {
            // Arrange.
            A.CallTo(() => _bus.Call<GetLayouts, LayoutsResponse>(null)).WithAnyArguments()
                .Returns(new LayoutsResponse
                {
                    Layouts = new List<LayoutDto>
                    {
                        _fixture.Create<LayoutDto>()
                    }
                });
            var request = _fixture.Create<int>();

            // Act
            await _controller.GetLayoutAsync(request);

            // Assert.
            A.CallTo(() => _bus.Call<GetLayouts, LayoutsResponse>
                    (A<GetLayouts>.That.Matches(x => x.Filter.Id == request)))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void DeleteLayoutsAsync_ShouldTouchDeleteLayouts()
        {
            // Arrange.
            var request = _fixture.Create<int>();

            // Act
            await _controller.DeleteLayoutAsync(request);

            // Assert.
            A.CallTo(() => _bus.Call<DeleteLayout, SuccessResponse>
                    (A<DeleteLayout>.That.Matches(x => x.Id == request)))
                .MustHaveHappenedOnceExactly();
        }
    }
}