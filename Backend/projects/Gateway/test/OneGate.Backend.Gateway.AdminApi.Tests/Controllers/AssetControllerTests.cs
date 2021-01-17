using FakeItEasy;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.AdminApi.Controllers;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Asset;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Common.Models.Asset;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Xunit;

namespace OneGate.Backend.Gateway.AdminApi.Tests.Controllers
{
    public class AssetControllerTests
    {
        private readonly Fixture _fixture;

        private readonly IOgBus _bus;
        private readonly ILogger<AssetsController> _logger;

        private readonly AssetsController _controller;

        public AssetControllerTests()
        {
            _fixture = new Fixture();
            _bus = A.Fake<IOgBus>();
            _logger = A.Fake<ILogger<AssetsController>>();
            _controller = new AssetsController(_logger, _bus);
        }

        [Fact]
        public async void CreateAssetAsync_ShouldTouchCreateAsset()
        {
            // Arrange.
            _fixture.Customizations.Add(new TypeRelay(typeof(CreateAssetDto), typeof(CreateStockAssetDto)));
            var request = _fixture.Create<CreateAssetDto>();

            // Act.
            await _controller.CreateAssetAsync(request);

            // Assert.
            A.CallTo(() =>
                    _bus.Call<CreateAsset, CreatedResourceResponse>(
                        A<CreateAsset>.That.Matches(x => x.Asset == request)))
                .MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public async void DeleteAssetsAsync_ShouldTouchDeleteAssets()
        {
            // Arrange.
            var request = _fixture.Create<int>();

            // Act
            await _controller.DeleteAssetAsync(request);

            // Assert.
            A.CallTo(() => _bus.Call<DeleteAsset, SuccessResponse>
                    (A<DeleteAsset>.That.Matches(x => x.Id == request)))
                .MustHaveHappenedOnceExactly();
        }
    }
}