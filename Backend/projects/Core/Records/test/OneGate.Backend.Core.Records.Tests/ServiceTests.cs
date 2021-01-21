using System.Collections.Generic;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using OneGate.Backend.Core.Records;
using OneGate.Backend.Core.Records.Database.Models;
using OneGate.Backend.Core.Records.Database.Repository;
using OneGate.Backend.Transport.Dto.Asset;
using OneGate.Backend.Transport.Contracts.Asset;
using OneGate.Backend.Transport.Contracts.Exchange;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Xunit;

namespace OneGate.Backend.Records.Tests
{
    public class ServiceTests
    {
        private readonly Mapper _mapper;
        private readonly Fixture _fixture;

        private readonly List<Asset> _assetRepository;
        private readonly List<Exchange> _exchangeRepository;
        private readonly List<Layout> _layoutRepository;

        private readonly IService _service;

        public ServiceTests()
        {
            _fixture = new Fixture();
            _mapper = new Mapper(new MapperConfiguration(x =>
                x.AddProfile(typeof(MappingProfile)))
            );

            var assets = A.Fake<IAssetRepository>();
            _assetRepository = ConfigureFakeRepository(assets);

            var exchanges = A.Fake<IExchangeRepository>();
            _exchangeRepository = ConfigureFakeRepository(exchanges);

            var layouts = A.Fake<ILayoutRepository>();
            _layoutRepository = ConfigureFakeRepository(layouts);

            _service = new Service(assets, exchanges, layouts, _mapper);
        }

        private static List<Asset> ConfigureFakeRepository(IAssetRepository assets)
        {
            var fakeRepository = new List<Asset>();

            A.CallTo(() => assets.AddAsync(A<Asset>.Ignored))
                .Invokes((Asset x) =>
                {
                    fakeRepository.Add(x);
                })
                .ReturnsLazily((Asset x) => x);

            A.CallTo(() => assets.RemoveAsync(A<int>.Ignored))
                .Invokes((int x) =>
                {
                    fakeRepository.RemoveAll(elem => elem.Id == x);
                });

            return fakeRepository;
        }

        private static List<Exchange> ConfigureFakeRepository(IExchangeRepository assets)
        {
            var fakeRepository = new List<Exchange>();

            A.CallTo(() => assets.AddAsync(A<Exchange>.Ignored))
                .Invokes((Exchange x) =>
                {
                    fakeRepository.Add(x);
                })
                .ReturnsLazily((Exchange x) => x);

            A.CallTo(() => assets.RemoveAsync(A<int>.Ignored))
                .Invokes((int x) =>
                {
                    fakeRepository.RemoveAll(elem => elem.Id == x);
                });

            return fakeRepository;
        }

        private static List<Layout> ConfigureFakeRepository(ILayoutRepository assets)
        {
            var fakeRepository = new List<Layout>();

            A.CallTo(() => assets.AddAsync(A<Layout>.Ignored))
                .Invokes((Layout x) =>
                {
                    fakeRepository.Add(x);
                })
                .ReturnsLazily((Layout x) => x);

            A.CallTo(() => assets.RemoveAsync(A<int>.Ignored))
                .Invokes((int x) =>
                {
                    fakeRepository.RemoveAll(elem => elem.Id == x);
                });

            return fakeRepository;
        }

        [Fact]
        public async void CreateAsset_ShouldCreateEntityInRepository()
        {
            // Arrange.
            _fixture.Customizations.Add(new TypeRelay(typeof(CreateAssetDto), typeof(CreateIndexAssetDto)));
            var request = _fixture.Create<CreateAsset>();

            // Act.
            await _service.CreateAssetAsync(request);

            // Assert.
            _assetRepository.Should().ContainSingle().Which.Should().BeEquivalentTo
                (request.Asset, options => options.ComparingEnumsByName());
        }

        [Fact]
        public async void DeleteAsset_ShouldRemoveEntityFromRepository()
        {
            // Arrange.
            var request = _fixture.Create<DeleteAsset>();

            _assetRepository.Add(_fixture.Create<IndexAsset>());
            _assetRepository.Add(_fixture.Build<IndexAsset>()
                .With(x => x.Id, request.Id)
                .Create());

            // Act.
            await _service.DeleteAssetAsync(request);

            // Assert.
            _assetRepository.Should().NotContain(x => x.Id == request.Id);
        }

        [Fact]
        public async void CreateExchange_ShouldCreateEntityInRepository()
        {
            // Arrange.
            var request = _fixture.Create<CreateExchange>();

            // Act.
            await _service.CreateExchangeAsync(request);

            // Assert.
            _exchangeRepository.Should().ContainSingle().Which.Should().BeEquivalentTo(request.Exchange,
                options => options.ComparingEnumsByName());
        }

        [Fact]
        public async void DeleteExchange_ShouldRemoveEntityFromRepository()
        {
            // Arrange.
            var request = _fixture.Create<DeleteExchange>();

            _exchangeRepository.Add(_fixture.Create<Exchange>());
            _exchangeRepository.Add(_fixture.Build<Exchange>()
                    .With(x => x.Id, request.Id)
                    .Create());

            // Act.
            await _service.DeleteExchangeAsync(request);

            // Assert.
            _exchangeRepository.Should().NotContain(x => x.Id == request.Id);
        }
    }
}