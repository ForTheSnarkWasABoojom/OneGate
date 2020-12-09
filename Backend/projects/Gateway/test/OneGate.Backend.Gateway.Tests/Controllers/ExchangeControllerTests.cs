using System.Collections.Generic;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.Controllers;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Exchange;
using OneGate.Common.Models.Exchange;
using Ploeh.AutoFixture;
using Xunit;

namespace OneGate.Backend.Gateway.Tests.Controllers
{
    public class ExchangeControllerTests
    {
        private readonly Fixture _fixture;

        private readonly IOgBus _bus;
        private readonly ILogger<ExchangeController> _logger;

        private readonly ExchangeController _controller;

        public ExchangeControllerTests()
        {
            _fixture = new Fixture();
            _bus = A.Fake<IOgBus>();
            _logger = A.Fake<ILogger<ExchangeController>>();
            _controller = new ExchangeController(_logger, _bus);
        }

        [Fact]
        public async void CreateExchangeAsync_ShouldTouchCreateExchange()
        {
            // Arrange.
            var request = _fixture.Create<CreateExchangeDto>();

            // Act.
            await _controller.CreateExchangeAsync(request);

            // Assert.
            A.CallTo(() =>
                    _bus.Call<CreateExchange, CreatedResourceResponse>(
                        A<CreateExchange>.That.Matches(x => x.Exchange == request)))
                .MustHaveHappenedOnceExactly();
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

        [Fact]
        public async void DeleteExchangesAsync_ShouldTouchDeleteExchanges()
        {
            // Arrange.
            var request = _fixture.Create<int>();

            // Act
            await _controller.DeleteExchangeAsync(request);

            // Assert.
            A.CallTo(() => _bus.Call<DeleteExchange, SuccessResponse>
                    (A<DeleteExchange>.That.Matches(x => x.Id == request)))
                .MustHaveHappenedOnceExactly();
        }
    }
}