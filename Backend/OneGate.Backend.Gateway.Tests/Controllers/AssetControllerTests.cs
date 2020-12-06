using FakeItEasy;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Contracts.Asset;
using OneGate.Backend.Contracts.Common;
using OneGate.Backend.Gateway.Controllers;
using OneGate.Backend.Rpc;
using OneGate.Shared.Models.Asset;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Xunit;

namespace OneGate.Backend.Gateway.Tests.Controllers
{
    public class AssetControllerTests
    {
        private readonly Fixture _fixture;

        private readonly IOgBus _bus;
        private readonly ILogger<AssetController> _logger;
        
        private readonly AssetController _controller;

        public AssetControllerTests()
        {
            _fixture = new Fixture();
            _bus = A.Fake<IOgBus>();
            _logger = A.Fake<ILogger<AssetController>>();
            _controller = new AssetController(_logger, _bus);
        }

        [Fact]
        public async void CreateAssetAsync_ShouldTouchCreateAsset()
        {
            // Arrange.
            _fixture.Customizations.Add(new TypeRelay(typeof(CreateAssetDto), typeof(CreateStockAssetDto)));
            var request = _fixture.Create<CreateAssetDto>();

            // Act.
            await _controller.CreateAssetAsync(request);

            // Asset.
            A.CallTo(() => _bus.Call<CreateAsset, CreatedResourceResponse>(A<CreateAsset>.That.Matches(x => x.Asset == request)))
                .MustHaveHappenedOnceExactly();
        }
    }
}