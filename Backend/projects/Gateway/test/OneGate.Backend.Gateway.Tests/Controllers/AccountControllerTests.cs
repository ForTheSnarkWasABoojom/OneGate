using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.Controllers;
using OneGate.Backend.Gateway.Extensions;
using OneGate.Backend.Gateway.Middleware;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Account;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Common.Models.Account;
using Ploeh.AutoFixture;
using Shouldly;
using Xunit;

namespace OneGate.Backend.Gateway.Tests.Controllers
{
    public class AccountControllerTests
    {
        private readonly Fixture _fixture;

        private readonly IOgBus _bus;
        private readonly ILogger<AccountController> _logger;
        private readonly IAuthCredentials _auth;
        private readonly ClaimsPrincipal _user;

        private readonly AccountController _controller;

        public AccountControllerTests()
        {
            _fixture = new Fixture();
            _bus = A.Fake<IOgBus>();
            _logger = A.Fake<ILogger<AccountController>>();
            _auth = A.Fake<IAuthCredentials>();
            A.CallTo(() => _auth.ClientKey).Returns("test_client_key");
            _controller = new AccountController(_logger, _bus, _auth);
            _user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "default_user"),
                new Claim(ClaimTypes.NameIdentifier, "1")
            }));
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext {User = _user}
            };
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

            // Assert.
            A.CallTo(() => _bus.Call<CreateAccount, CreatedResourceResponse>
                    (A<CreateAccount>.That.Matches(x => x.Account == request)))
                .MustHaveHappenedOnceExactly();
        }


        [Fact]
        public async void CreateAccountAsync_ShouldThrowException_IfClientKeyIsInvalid()
        {
            // Arrange.
            var request = _fixture.Create<CreateAccountDto>();

            // Act.
            Task Act() => _controller.CreateAccountAsync(request, new ClientKeyDto
            {
                ClientKey = "wrong_client_key"
            });

            // Assert.
            await Should.ThrowAsync<ApiException>(Act);
        }

        [Fact]
        public async void GetAccountsAsync_ShouldTouchGetAccounts()
        {
            // Arrange.
            var request = _fixture.Create<AccountFilterDto>();

            // Act
            await _controller.GetAccountsRangeAsync(request);

            // Assert.
            A.CallTo(() => _bus.Call<GetAccounts, AccountsResponse>
                    (A<GetAccounts>.That.Matches(x => x.Filter == request)))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void GetAccountAsync_ShouldTouchGetAccount()
        {
            // Arrange.
            A.CallTo(() => _bus.Call<GetAccounts, AccountsResponse>(null)).WithAnyArguments()
                .Returns(new AccountsResponse
                {
                    Accounts = new List<AccountDto>
                    {
                        _fixture.Create<AccountDto>()
                    }
                });
            var request = _fixture.Create<int>();

            // Act
            await _controller.GetAccountAsync(request);

            // Assert.
            A.CallTo(() => _bus.Call<GetAccounts, AccountsResponse>
                    (A<GetAccounts>.That.Matches(x => x.Filter.Id == request)))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void GetMytAccountAsync_ShouldTouchGetMyAccount()
        {
            // Arrange 
            A.CallTo(() => _bus.Call<GetAccounts, AccountsResponse>(null)).WithAnyArguments()
                .Returns(new AccountsResponse
                {
                    Accounts = new List<AccountDto>
                    {
                        _fixture.Create<AccountDto>()
                    }
                });

            // Act
            await _controller.GetMyAccountAsync();

            // Assert.
            A.CallTo(() => _bus.Call<GetAccounts, AccountsResponse>
                    (A<GetAccounts>.That.Matches(x => x.Filter.Id == _user.GetAccountId())))
                .MustHaveHappenedOnceExactly();
        }


        [Fact]
        public async void DeleteAccountsAsync_ShouldTouchDeleteAccounts()
        {
            // Arrange.
            var request = _fixture.Create<int>();

            // Act
            await _controller.DeleteAccountAsync(request);

            // Assert.
            A.CallTo(() => _bus.Call<DeleteAccount, SuccessResponse>
                    (A<DeleteAccount>.That.Matches(x => x.Id == request)))
                .MustHaveHappenedOnceExactly();
        }
    }
}