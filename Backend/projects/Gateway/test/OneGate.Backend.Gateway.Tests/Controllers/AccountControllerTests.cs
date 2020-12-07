using FakeItEasy;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.Controllers;
using OneGate.Backend.Gateway.Middleware;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Account;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Common.Models.Account;
using Ploeh.AutoFixture;
using Xunit;

namespace OneGate.Backend.Gateway.Tests.Controllers
{
    public class AccountControllerTests
    {
        private readonly Fixture _fixture;

        private readonly IOgBus _bus;
        private readonly ILogger<AccountController> _logger;
        private readonly IAuthCredentials _auth;

        private readonly AccountController _controller;

        public AccountControllerTests()
        {
            _fixture = new Fixture();
            _bus = A.Fake<IOgBus>();
            _logger = A.Fake<ILogger<AccountController>>();
            _auth = A.Fake<IAuthCredentials>();
            _controller = new AccountController(_logger, _bus, _auth);
        }

        [Fact]
        public async void CreateAccountAsync_ShouldTouchCreateAccount()
        {
            // Arrange.
            var request = _fixture.Create<CreateAccountDto>();

            // Act.
            await _controller.CreateAccountAsync(request, new ClientKeyDto
            {
                ClientKey = _auth.ClientKey
            });

            // Asset.
            A.CallTo(() => _bus.Call<CreateAccount, CreatedResourceResponse>
                    (A<CreateAccount>.That.Matches(x => x.Account == request)))
                .MustHaveHappenedOnceExactly();
        }
    }
}