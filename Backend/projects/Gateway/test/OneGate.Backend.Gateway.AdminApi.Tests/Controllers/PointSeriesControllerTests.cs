using FakeItEasy;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.AdminApi.Controllers;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Series.Point;
using OneGate.Shared.ApiContracts.Series.Point;
using Ploeh.AutoFixture;
using Xunit;

namespace OneGate.Backend.Gateway.AdminApi.Tests.Controllers
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
        public async void CreatePointSeriesAsync_ShouldTouchCreatePointSeries()
        {
            // Arrange.
            var request = _fixture.Create<PointSeriesDto>();

            // Act.
            await _controller.CreatePointSeriesAsync(request);

            // Assert.
            A.CallTo(() =>
                    _bus.Call<CreatePointSeries, SuccessResponse>(
                        A<CreatePointSeries>.That.Matches(x => x.Series == request)))
                .MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public async void DeletePointSeriesAsync_ShouldTouchDeletePointSeries()
        {
            // Arrange.
            var request = _fixture.Create<PointSeriesFilterDto>();

            // Act
            await _controller.DeletePointSeriesAsync(request);

            // Assert.
            A.CallTo(() => _bus.Call<DeletePointSeries, SuccessResponse>
                    (A<DeletePointSeries>.That.Matches(x => x.Filter == request)))
                .MustHaveHappenedOnceExactly();
        }
    }
}