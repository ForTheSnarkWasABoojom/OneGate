using System.Collections.Generic;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.UserApi.Controllers;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Asset;
using OneGate.Common.Models.Asset;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Xunit;

namespace OneGate.Backend.Gateway.UserApi.Tests.Controllers
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
        public async void GetAssetsAsync_ShouldTouchGetAssets()
        {
            // Arrange.
            var request = _fixture.Create<AssetFilterDto>();

            // Act
            await _controller.GetAssetsRangeAsync(request);

            // Assert.
            A.CallTo(() => _bus.Call<GetAssets, AssetsResponse>
                    (A<GetAssets>.That.Matches(x => x.Filter == request)))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void GetAssetAsync_ShouldTouchGetAsset()
        {
            // Arrange.
            _fixture.Customizations.Add(new TypeRelay(typeof(AssetDto), typeof(StockAssetDto)));
            A.CallTo(() => _bus.Call<GetAssets, AssetsResponse>(null)).WithAnyArguments()
                .Returns(new AssetsResponse
                {
                    Assets = new List<AssetDto>
                    {
                        _fixture.Create<AssetDto>()
                    }
                });
            var request = _fixture.Create<int>();

            // Act
            await _controller.GetAssetAsync(request);

            // Assert.
            A.CallTo(() => _bus.Call<GetAssets, AssetsResponse>
                    (A<GetAssets>.That.Matches(x => x.Filter.Id == request)))
                .MustHaveHappenedOnceExactly();
        }
    }
}