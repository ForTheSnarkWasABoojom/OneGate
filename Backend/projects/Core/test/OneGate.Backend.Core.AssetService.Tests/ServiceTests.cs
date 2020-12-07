using FakeItEasy;
using OneGate.Backend.Core.AssetService;
using OneGate.Backend.Core.AssetService.Repository;
using OneGate.Backend.Transport.Contracts.Asset;
using OneGate.Backend.Transport.Contracts.Exchange;
using OneGate.Common.Models.Asset;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Xunit;

namespace OneGate.Backend.Services.AssetService.Tests
{
    public class ServiceTests
    {
        private readonly Fixture _fixture;
        private readonly IAssetRepository _assetsRepository;
        private readonly IExchangeRepository _exchangesRepository;
        private readonly ILayoutRepository _layoutRepository;

        private readonly IService _service;
        public ServiceTests()
        {
            _fixture = new Fixture();
            
            _assetsRepository = A.Fake<IAssetRepository>();
            _exchangesRepository = A.Fake<IExchangeRepository>();
            _layoutRepository = A.Fake<ILayoutRepository>();
                
            _service = new Service(_assetsRepository, _exchangesRepository,_layoutRepository);
        }
        
        [Fact]
        public async void CreateAsset_ShouldTouchRepositoryAdd()
        {
            // Arrange.
            _fixture.Customizations.Add(new TypeRelay(typeof(CreateAssetDto
                ), typeof(CreateIndexAssetDto)));
            var request = _fixture.Create<CreateAsset>();

            // Act.
            await _service.CreateAsset(request);

            // Assert.
            A.CallTo(() => _assetsRepository.AddAsync(request.Asset)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void GetAssetsRange_ShouldTouchRepositoryFilter()
        {
            // Arrange.
            var request = _fixture.Create<GetAssets>();

            // Act.
            await _service.GetAssets(request);

            // Assert.
            A.CallTo(() => _assetsRepository.FilterAsync(request.Filter)).MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public async void DeleteAsset_ShouldTouchRepositoryRemove()
        {
            // Arrange.
            var request = _fixture.Create<DeleteAsset>();

            // Act.
            await _service.DeleteAsset(request);

            // Assert.
            A.CallTo(() => _assetsRepository.RemoveAsync(request.Id)).MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public async void CreateExchange_ShouldTouchRepositoryAdd()
        {
            // Arrange.
            var request = _fixture.Create<CreateExchange>();

            // Act.
            await _service.CreateExchange(request);

            // Assert.
            A.CallTo(() => _exchangesRepository.AddAsync(request.Exchange)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void GetExchangesRange_ShouldTouchRepositoryFilter()
        {
            // Arrange.
            var request = _fixture.Create<GetExchanges>();

            // Act.
            await _service.GetExchanges(request);

            // Assert.
            A.CallTo(() => _exchangesRepository.FilterAsync(request.Filter)).MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public async void DeleteExchange_ShouldTouchRepositoryRemove()
        {
            // Arrange.
            var request = _fixture.Create<DeleteExchange>();

            // Act.
            await _service.DeleteExchange(request);

            // Assert.
            A.CallTo(() => _exchangesRepository.RemoveAsync(request.Id)).MustHaveHappenedOnceExactly();
        }
    }
}