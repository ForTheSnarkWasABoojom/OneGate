using System.Collections.Generic;
using AutoMapper;
using Xunit;
using FakeItEasy;
using FluentAssertions;
using OneGate.Backend.Core.Users.Database.Models;
using OneGate.Backend.Core.Users.Database.Repository;
using OneGate.Backend.Transport.Contracts.Account;
using OneGate.Backend.Transport.Contracts.Order;
using OneGate.Backend.Transport.Contracts.Portfolio;
using OneGate.Shared.ApiContracts.Order;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace OneGate.Backend.Core.Users.Node.Tests
{
    public class ServiceTests
    {
        private readonly Fixture _fixture;
        private readonly Mapper _mapper;

        private readonly List<Account> _accountRepository;
        private readonly List<Order> _orderRepository;
        private readonly List<Portfolio> _portfolioRepository;

        private readonly IService _service;

        public ServiceTests()
        {
            _fixture = new Fixture();
            _mapper = new Mapper(new MapperConfiguration(x =>
                x.AddProfile(typeof(MappingProfile)))
            );

            var accounts = A.Fake<IAccountRepository>();
            _accountRepository = ConfigureFakeRepository(accounts);
            
            var orders = A.Fake<IOrderRepository>();
            _orderRepository = ConfigureFakeRepository(orders);
            
            var portfolios = A.Fake<IPortfolioRepository>();
            _portfolioRepository = ConfigureFakeRepository(portfolios);

            _service = new Service(_mapper, accounts, orders, portfolios);
        }
        
        private static List<Account> ConfigureFakeRepository(IAccountRepository assets)
        {
            var fakeRepository = new List<Account>();

            A.CallTo(() => assets.AddAsync(A<Account>.Ignored))
                .Invokes((Account x) =>
                {
                    fakeRepository.Add(x);
                })
                .ReturnsLazily((Account x) => x);

            A.CallTo(() => assets.RemoveAsync(A<int>.Ignored))
                .Invokes((int x) =>
                {
                    fakeRepository.RemoveAll(elem => elem.Id == x);
                });

            return fakeRepository;
        }
        
        private static List<Order> ConfigureFakeRepository(IOrderRepository orders)
        {
            var fakeRepository = new List<Order>();

            A.CallTo(() => orders.AddAsync(A<Order>.Ignored))
                .Invokes((Order x) =>
                {
                    fakeRepository.Add(x);
                })
                .ReturnsLazily((Order x) => x);

            A.CallTo(() => orders.RemoveAsync(A<int>.Ignored, A<int>.Ignored))
                .Invokes((int id, int ownerId) =>
                {
                    fakeRepository.RemoveAll(elem => elem.Id == id && elem.OwnerId == ownerId);
                });

            return fakeRepository;
        }
        
        private static List<Portfolio> ConfigureFakeRepository(IPortfolioRepository portfolios)
        {
            var fakeRepository = new List<Portfolio>();

            A.CallTo(() => portfolios.AddAsync(A<Portfolio>.Ignored))
                .Invokes((Portfolio x) =>
                {
                    fakeRepository.Add(x);
                })
                .ReturnsLazily((Portfolio x) => x);

            A.CallTo(() => portfolios.RemoveAsync(A<int>.Ignored, A<int>.Ignored))
                .Invokes((int id, int ownerId) =>
                {
                    fakeRepository.RemoveAll(elem => elem.Id == id && elem.OwnerId == ownerId);
                });

            return fakeRepository;
        }

        [Fact]
        public async void CreateAccountAsync_ShouldCreateEntityInRepository()
        {
            // Arrange.
            var request = _fixture.Create<CreateAccount>();

            // Act.
            await _service.CreateAccountAsync(request);

            // Assert.
            _accountRepository.Should().ContainSingle().Which.Should().BeEquivalentTo(request.Account);
        }

        [Fact]
        public async void DeleteAccountAsync_ShouldRemoveEntityFromRepository()
        {
            // Arrange.
            var request = _fixture.Create<DeleteAccount>();

            _accountRepository.Add(_fixture.Create<Account>());
            _accountRepository.Add(_fixture.Build<Account>()
                .With(x => x.Id, request.Id)
                .Create());
            
            // Act.
            await _service.DeleteAccountAsync(request);

            // Assert.
            _accountRepository.Should().NotContain(x => x.Id == request.Id);
        }

        [Fact]
        public async void CreateOrderAsync_ShouldCreateEntityInRepository()
        {
            // Arrange.
            _fixture.Customizations.Add(new TypeRelay(typeof(CreateOrderDto), typeof(CreateMarketOrderDto)));
            var request = _fixture.Create<CreateOrder>();

            // Act.
            await _service.CreateOrderAsync(request);

            // Assert.
            _orderRepository.Should().ContainSingle().Which.Should().BeEquivalentTo
                (request.Order, options => options.ComparingEnumsByName());
        }

        [Fact]
        public async void DeleteOrderAsync_ShouldRemoveEntityFromRepository()
        {
            // Arrange.
            var request = _fixture.Create<DeleteOrder>();
            
            _orderRepository.Add(_fixture.Create<LimitOrder>());
            _orderRepository.Add(_fixture.Build<LimitOrder>()
                .With(x => x.Id, request.Id)
                .With(x => x.OwnerId, request.OwnerId)
                .Create());

            // Act.
            await _service.DeleteOrderAsync(request);

            // Assert.
            _orderRepository.Should().NotContain(x => x.Id == request.Id && x.OwnerId == request.OwnerId);
        }
        
        [Fact]
        public async void CreatePortfolioAsync_ShouldCreateEntityInRepository()
        {
            // Arrange.
            var request = _fixture.Create<CreatePortfolio>();

            // Act.
            await _service.CreatePortfolioAsync(request);

            // Assert.
            _portfolioRepository.Should().ContainSingle().Which.Should().BeEquivalentTo
                (request.Portfolio, options => options.ComparingEnumsByName());
        }

        [Fact]
        public async void DeletePortfolioAsync_ShouldRemoveEntityFromRepository()
        {
            // Arrange.
            var request = _fixture.Create<DeletePortfolio>();
            
            _portfolioRepository.Add(_fixture.Create<Portfolio>());
            _portfolioRepository.Add(_fixture.Build<Portfolio>()
                .With(x => x.Id, request.Id)
                .With(x => x.OwnerId, request.OwnerId)
                .Create());

            // Act.
            await _service.DeletePortfolioAsync(request);

            // Assert.
            _portfolioRepository.Should().NotContain(x => x.Id == request.Id && x.OwnerId == request.OwnerId);
        }
    }
}