using FakeItEasy;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.AdminApi.Controllers;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Series.Ohlc;
using OneGate.Common.Models.Series.Ohlc;
using Ploeh.AutoFixture;
using Xunit;

namespace OneGate.Backend.Gateway.AdminApi.Tests.Controllers
{
    public class OhlcSeriesControllerTests
    {
        private readonly Fixture _fixture;

        private readonly IOgBus _bus;
        private readonly ILogger<OhlcSeriesController> _logger;

        private readonly OhlcSeriesController _controller;

        public OhlcSeriesControllerTests()
        {
            _fixture = new Fixture();
            _bus = A.Fake<IOgBus>();
            _logger = A.Fake<ILogger<OhlcSeriesController>>();
            _controller = new OhlcSeriesController(_logger, _bus);
        }

        [Fact]
        public async void CreateOhlcSeriesAsync_ShouldTouchCreateOhlcSeries()
        {
            // Arrange.
            var request = _fixture.Create<OhlcSeriesDto>();

            // Act.
            await _controller.CreateOhlcSeriesAsync(request);

            // Assert.
            A.CallTo(() => _bus.Call<CreateOhlcSeries, SuccessResponse>
                    (A<CreateOhlcSeries>.That.Matches(x => x.Series == request)))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void DeleteOhlcSeriesAsync_ShouldTouchDeleteOhlcSeries()
        {
            // Arrange.
            var request = _fixture.Create<OhlcSeriesFilterDto>();

            // Act
            await _controller.DeleteOhlcSeriesAsync(request);

            // Assert.
            A.CallTo(() => _bus.Call<DeleteOhlcSeries, SuccessResponse>
                    (A<DeleteOhlcSeries>.That.Matches(x => x.Filter == request)))
                .MustHaveHappenedOnceExactly();
        }
    }
}