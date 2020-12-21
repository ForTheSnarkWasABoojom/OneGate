using System.Collections.Generic;
using AutoMapper;
using Xunit;
using FakeItEasy;
using FluentAssertions;
using OneGate.Backend.Core.Users.Database.Models;
using OneGate.Backend.Core.Users.Database.Repository;
using OneGate.Backend.Transport.Contracts.Account;
using OneGate.Backend.Transport.Contracts.Order;
using OneGate.Common.Models.Order;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace OneGate.Backend.Core.Users.Node.Tests
{
    public class ServiceTests
    {
        private readonly Fixture _fixture;
        private readonly Mapper _mapper;

        private readonly IAccountRepository _accountRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IPorfolioAssetLinkRepository _portfolioAssetLinkRepository;

        private readonly IService _service;

        public ServiceTests()
        {
            _fixture = new Fixture();
            _mapper = new Mapper(new MapperConfiguration(x =>
                x.AddProfile(typeof(MappingProfile)))
            );

            _accountRepository = A.Fake<IAccountRepository>();
            _orderRepository = A.Fake<IOrderRepository>();
            _portfolioRepository = A.Fake<IPortfolioRepository>();
            _portfolioAssetLinkRepository = A.Fake<IPorfolioAssetLinkRepository>();

            _service = new Service(_mapper, _accountRepository, _orderRepository, _portfolioRepository,
                _portfolioAssetLinkRepository);
        }

        [Fact]
        public async void CreateAccount_ShouldCreateEntityInRepository()
        {
            // Arrange.
            var request = _fixture.Create<CreateAccount>();
            var fakeRepository = new List<Account>();
            A.CallTo(() => _accountRepository.AddAsync(A<Account>.Ignored))
                .Invokes((Account x) => { fakeRepository.Add(x); })
                .Returns(0);

            // Act.
            await _service.CreateAccountAsync(request);

            // Assert.
            fakeRepository.Should().ContainSingle().Which.Should().BeEquivalentTo(request.Account);
        }


        [Fact]
        public async void GetAccounts_ShouldContainExpectedAccount()
        {
            // Arrange.
            var request = _fixture.Create<GetAccounts>();
            var expectedAccount =
                _fixture.Build<Account>()
                    .With(x => x.FirstName, request.Filter.FirstName)
                    .With(x => x.LastName, request.Filter.LastName)
                    .With(x => x.Email, request.Filter.Email)
                    .With(x => x.IsAdmin, request.Filter.IsAdmin)
                    .Create();
            var fakeRepository = new List<Account>
            {
                _fixture.Create<Account>(),
                expectedAccount
            };
            A.CallTo(() => _accountRepository.FilterAsync(A<int?>.Ignored, A<string>.Ignored
                    , A<string>.Ignored, A<string>.Ignored, A<bool?>.Ignored,
                    A<int>.Ignored, A<int>.Ignored))
                .Invokes((int? id, string email, string firstName,
                        string lastName, bool? isAdmin, int shift, int count) =>
                    fakeRepository.FindAll(account => account.FirstName == firstName));
            // Act.
            await _service.GetAccountsAsync(request);

            // Assert.
            fakeRepository.Should().Contain(x => x.FirstName == expectedAccount.FirstName);
        }

        [Fact]
        public async void DeleteAccount_ShouldRemoveEntityFromRepository()
        {
            // Arrange.
            var request = _fixture.Create<DeleteAccount>();

            var fakeRepository = new List<Account>
            {
                _fixture.Create<Account>(),
                _fixture.Build<Account>()
                    .With(x => x.Id, request.Id)
                    .Create()
            };

            A.CallTo(() => _accountRepository.RemoveAsync(A<int>.Ignored))
                .Invokes((int x) => { fakeRepository.RemoveAll(account => account.Id == x); });

            // Act.
            await _service.DeleteAccountAsync(request);

            // Assert.
            fakeRepository.Should().NotContain(x => x.Id == request.Id);
        }

        [Fact]
        public async void CreateOrder_ShouldCreateEntityInRepository()
        {
            // Arrange.
            _fixture.Customizations.Add(new TypeRelay(typeof(CreateOrderDto), typeof(CreateMarketOrderDto)));
            var request = _fixture.Create<CreateOrder>();
            var fakeRepository = new List<Order>();

            A.CallTo(() => _orderRepository.AddAsync(A<Order>.Ignored))
                .Invokes((Order x) => { fakeRepository.Add(x); })
                .Returns(0);

            // Act.
            await _service.CreateOrderAsync(request);

            // Assert.
            fakeRepository.Should().ContainSingle().Which.Should().BeEquivalentTo
                (request.Order, options => options.ComparingEnumsByName());
        }

        [Fact]
        public async void DeleteOrder_ShouldRemoveEntityFromRepository()
        {
            // Arrange.
            var request = _fixture.Create<DeleteOrder>();
            var fakeRepository = new List<Order>
            {
                _fixture.Create<LimitOrder>(),
                _fixture.Build<LimitOrder>()
                    .With(x => x.Id, request.Id)
                    .With(x => x.OwnerId, request.OwnerId)
                    .Create()
            };

            A.CallTo(() => _orderRepository.RemoveAsync(A<int>.Ignored, A<int>.Ignored))
                .Invokes((int id, int ownerId) =>
                {
                    fakeRepository.RemoveAll(order => order.Id == id && order.OwnerId == ownerId);
                });

            // Act.
            await _service.DeleteOrderAsync(request);

            // Assert.
            fakeRepository.Should().NotContain(x => x.Id == request.Id && x.OwnerId == request.OwnerId);
        }
    }
}