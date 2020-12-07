using FakeItEasy;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Contracts.Common;
using OneGate.Backend.Contracts.Exchange;
using OneGate.Backend.Gateway.Controllers;
using OneGate.Backend.Rpc;
using OneGate.Shared.Models.Exchange;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
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

            // Asset.
            A.CallTo(() => _bus.Call<CreateExchange, CreatedResourceResponse>(A<CreateExchange>.That.Matches(x => x.Exchange == request)))
                .MustHaveHappenedOnceExactly();
        }
    }
}