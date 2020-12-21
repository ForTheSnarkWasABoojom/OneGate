using System.Collections.Generic;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using OneGate.Backend.Core.Records.Database.Models;
using OneGate.Backend.Core.Records.Database.Repository;
using OneGate.Backend.Core.Records.Node;
using OneGate.Backend.Transport.Contracts.Asset;
using OneGate.Backend.Transport.Contracts.Exchange;
using OneGate.Common.Models.Asset;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Xunit;

namespace OneGate.Backend.Records.Node.Tests
{
    public class ServiceTests
    {
        private readonly Mapper _mapper;

        private readonly Fixture _fixture;
        private readonly IAssetRepository _assetsRepository;
        private readonly IExchangeRepository _exchangesRepository;
        private readonly ILayoutRepository _layoutRepository;

        private readonly IService _service;

        public ServiceTests()
        {
            _fixture = new Fixture();
            _mapper = new Mapper(new MapperConfiguration(x =>
                x.AddProfile(typeof(MappingProfile)))
            );

            _assetsRepository = A.Fake<IAssetRepository>();
            _exchangesRepository = A.Fake<IExchangeRepository>();
            _layoutRepository = A.Fake<ILayoutRepository>();

            _service = new Service(_assetsRepository, _exchangesRepository, _layoutRepository, _mapper);
        }


        [Fact]
        public async void CreateAsset_ShouldCreateEntityInRepository()
        {
            // Arrange.
            _fixture.Customizations.Add(new TypeRelay(typeof(CreateAssetDto), typeof(CreateIndexAssetDto)));
            var request = _fixture.Create<CreateAsset>();
            var fakeRepository = new List<Asset>();

            A.CallTo(() => _assetsRepository.AddAsync(A<Asset>.Ignored))
                .Invokes((Asset x) => { fakeRepository.Add(x); })
                .Returns(0);

            // Act.
            await _service.CreateAssetAsync(request);

            // Assert.
            fakeRepository.Should().ContainSingle().Which.Should().BeEquivalentTo
                (request.Asset, options => options.ComparingEnumsByName());
        }
        
        [Fact]
        public async void DeleteAsset_ShouldRemoveEntityFromRepository()
        {
            // Arrange.
            var request = _fixture.Create<DeleteAsset>();

            var fakeRepository = new List<Asset>
            {
                _fixture.Create<IndexAsset>(),
                _fixture.Build<IndexAsset>()
                    .With(x => x.Id, request.Id)
                    .Create()
            };

            A.CallTo(() => _assetsRepository.RemoveAsync(A<int>.Ignored))
                .Invokes((int x) => { fakeRepository.RemoveAll(asset => asset.Id == x); });

            // Act.
            await _service.DeleteAssetAsync(request);

            // Assert.
            fakeRepository.Should().NotContain(x => x.Id == request.Id);
        }

        [Fact]
        public async void CreateExchange_ShouldCreateEntityInRepository()
        {
            // Arrange.
            var request = _fixture.Create<CreateExchange>();
            var fakeRepository = new List<Exchange>();
            A.CallTo(() => _exchangesRepository.AddAsync(A<Exchange>.Ignored))
                .Invokes((Exchange x) => { fakeRepository.Add(x); })
                .Returns(0);

            // Act.
            await _service.CreateExchangeAsync(request);

            // Assert.
            fakeRepository.Should().ContainSingle().Which.Should().BeEquivalentTo(request.Exchange,
                options => options.ComparingEnumsByName());
        }
        
        [Fact]
        public async void DeleteExchange_ShouldRemoveEntityFromRepository()
        {
            // Arrange.
            var request = _fixture.Create<DeleteExchange>();

            var fakeRepository = new List<Exchange>
            {
                _fixture.Create<Exchange>(),
                _fixture.Build<Exchange>()
                    .With(x => x.Id, request.Id)
                    .Create()
            };

            A.CallTo(() => _exchangesRepository.RemoveAsync(A<int>.Ignored))
                .Invokes((int x) => { fakeRepository.RemoveAll(exchange => exchange.Id == x); });

            // Act.
            await _service.DeleteExchangeAsync(request);

            // Assert.
            fakeRepository.Should().NotContain(x => x.Id == request.Id);
        }
    }
}