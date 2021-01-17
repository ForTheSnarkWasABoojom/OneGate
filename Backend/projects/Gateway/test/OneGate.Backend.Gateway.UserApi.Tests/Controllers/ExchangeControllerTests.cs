using System.Collections.Generic;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.UserApi.Controllers;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Exchange;
using OneGate.Common.Models.Exchange;
using Ploeh.AutoFixture;
using Xunit;

namespace OneGate.Backend.Gateway.UserApi.Tests.Controllers
{
    public class ExchangeControllerTests
    {
        private readonly Fixture _fixture;

        private readonly IOgBus _bus;
        private readonly ILogger<ExchangesController> _logger;

        private readonly ExchangesController _controller;

        public ExchangeControllerTests()
        {
            _fixture = new Fixture();
            _bus = A.Fake<IOgBus>();
            _logger = A.Fake<ILogger<ExchangesController>>();
            _controller = new ExchangesController(_logger, _bus);
        }

        [Fact]
        public async void GetExchangesAsync_ShouldTouchGetExchanges()
        {
            // Arrange.
            var request = _fixture.Create<ExchangeFilterDto>();

            // Act
            await _controller.GetExchangesRangeAsync(request);

            // Assert.
            A.CallTo(() => _bus.Call<GetExchanges, ExchangesResponse>
                    (A<GetExchanges>.That.Matches(x => x.Filter == request)))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void GetExchangeAsync_ShouldTouchGetExchange()
        {
            // Arrange.
            A.CallTo(() => _bus.Call<GetExchanges, ExchangesResponse>(null)).WithAnyArguments()
                .Returns(new ExchangesResponse
                {
                    Exchanges = new List<ExchangeDto>
                    {
                        _fixture.Create<ExchangeDto>()
                    }
                });
            var request = _fixture.Create<int>();

            // Act
            await _controller.GetExchangeAsync(request);

            // Assert.
            A.CallTo(() => _bus.Call<GetExchanges, ExchangesResponse>
                    (A<GetExchanges>.That.Matches(x => x.Filter.Id == request)))
                .MustHaveHappenedOnceExactly();
        }
    }
}