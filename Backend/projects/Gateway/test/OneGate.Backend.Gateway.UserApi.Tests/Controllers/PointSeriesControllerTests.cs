using FakeItEasy;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.UserApi.Controllers;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Series.Point;
using OneGate.Shared.ApiContracts.Series.Point;
using Ploeh.AutoFixture;
using Xunit;

namespace OneGate.Backend.Gateway.UserApi.Tests.Controllers
{
    public class PointSeriesControllerTests
    {
        private readonly Fixture _fixture;

        private readonly IOgBus _bus;
        private readonly ILogger<PointSeriesController> _logger;

        private readonly PointSeriesController _controller;

        public PointSeriesControllerTests()
        {
            _fixture = new Fixture();
            _bus = A.Fake<IOgBus>();
            _logger = A.Fake<ILogger<PointSeriesController>>();
            _controller = new PointSeriesController(_logger, _bus);
        }

        [Fact]
        public async void GetPointSeriesAsync_ShouldTouchGetPointSeries()
        {
            // Arrange.
            var request = _fixture.Create<PointSeriesFilterDto>();

            // Act
            await _controller.GetPointSeriesAsync(request);

            // Assert.
            A.CallTo(() => _bus.Call<GetPointSeries, PointSeriesResponse>
                    (A<GetPointSeries>.That.Matches(x => x.Filter == request)))
                .MustHaveHappenedOnceExactly();
        }
    }
}