using Xunit;
using FakeItEasy;
using OneGate.Backend.Contracts.Account;
using OneGate.Backend.Contracts.Order;
using OneGate.Backend.Services.AccountService.Repository;
using OneGate.Shared.Models.Order;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace OneGate.Backend.Services.AccountService.Tests
{
    public class ServiceTests
    {
        private readonly Fixture _fixture;

        private readonly IAccountRepository _accountRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IPorfolioAssetLinkRepository _portfolioAssetLinkRepository;
        
        private readonly IService _service;

        public ServiceTests()
        {
            _fixture = new Fixture();
            
            _accountRepository = A.Fake<IAccountRepository>();
            _orderRepository = A.Fake<IOrderRepository>();
            _portfolioRepository = A.Fake<IPortfolioRepository>();
            _portfolioAssetLinkRepository = A.Fake<IPorfolioAssetLinkRepository>();
            
            _service = new Service(_accountRepository, _orderRepository, _portfolioRepository, _portfolioAssetLinkRepository);
        }

        [Fact]
        public async void CreateAccount_ShouldTouchRepositoryAdd()
        {
            // Arrange.
            var request = _fixture.Create<CreateAccount>();

            // Act.
            await _service.CreateAccount(request);

            // Assert.
            A.CallTo(() => _accountRepository.AddAsync(request.Account)).MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public async void GetAccountsRange_ShouldTouchRepositoryFilter()
        {
            // Arrange.
            var request = _fixture.Create<GetAccounts>();

            // Act.
            await _service.GetAccounts(request);

            // Assert.
            A.CallTo(() => _accountRepository.FilterAsync(request.Filter)).MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public async void DeleteAccount_ShouldTouchRepositoryRemove()
        {
            // Arrange.
            var request = _fixture.Create<DeleteAccount>();

            // Act.
            await _service.DeleteAccount(request);

            // Assert.
            A.CallTo(() => _accountRepository.RemoveAsync(request.Id)).MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public async void CreateOrder_ShouldTouchRepositoryAdd()
        {
            // Arrange.
            _fixture.Customizations.Add(new TypeRelay(typeof(CreateOrderDto), typeof(CreateMarketOrderDto)));
            var request = _fixture.Create<CreateOrder>();

            // Act.
            await _service.CreateOrder(request);

            // Assert.
            A.CallTo(() => _orderRepository.AddAsync(request.Order, request.OwnerId)).MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public async void GetOrdersRange_ShouldTouchRepositoryFilter()
        {
            // Arrange.
            var request = _fixture.Create<GetOrders>();

            // Act.
            await _service.GetOrders(request);

            // Assert.
            A.CallTo(() => _orderRepository.FilterAsync(request.Filter, request.OwnerId)).MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public async void DeleteOrder_ShouldTouchRepositoryRemove()
        {
            // Arrange.
            var request = _fixture.Create<DeleteOrder>();

            // Act.
            await _service.DeleteOrder(request);

            // Assert.
            A.CallTo(() => _orderRepository.RemoveAsync(request.Id, request.OwnerId)).MustHaveHappenedOnceExactly();
        }
    }
}