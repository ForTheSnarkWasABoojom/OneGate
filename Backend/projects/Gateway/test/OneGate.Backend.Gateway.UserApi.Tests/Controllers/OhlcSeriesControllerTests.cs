using FakeItEasy;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.UserApi.Controllers;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Series.Ohlc;
using OneGate.Shared.ApiContracts.Series.Ohlc;
using Ploeh.AutoFixture;
using Xunit;

namespace OneGate.Backend.Gateway.UserApi.Tests.Controllers
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
        public async void GetOhlcSeriesAsync_ShouldTouchGetOhlcSeries()
        {
            // Arrange.
            var request = _fixture.Create<OhlcSeriesFilterDto>();

            // Act
            await _controller.GetOhlcSeriesAsync(request);

            // Assert.
            A.CallTo(() => _bus.Call<GetOhlcSeries, OhlcSeriesResponse>
                    (A<GetOhlcSeries>.That.Matches(x => x.Filter == request)))
                .MustHaveHappenedOnceExactly();
        }
    }
}