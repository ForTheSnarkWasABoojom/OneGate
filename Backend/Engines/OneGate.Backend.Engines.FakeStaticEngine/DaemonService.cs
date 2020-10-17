using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Engines.Base.OhlcProvider;
using OneGate.Backend.Rpc.Contracts.Asset.GetAssetsByFilter;
using OneGate.Backend.Rpc.Contracts.Timeseries;
using OneGate.Backend.Rpc.Services;
using OneGate.Shared.Models.Asset;
using OneGate.Shared.Models.Exchange;

namespace OneGate.Backend.Engines.FakeStaticEngine
{
    public class DaemonService : IHostedService
    {
        private readonly ILogger<DaemonService> _logger;
        private readonly IAssetService _assetService;
        private readonly IBus _bus;

        private List<IOhlcProvider> _ohlcProviders = new List<IOhlcProvider>();

        public DaemonService(ILogger<DaemonService> logger, IBus bus, IAssetService assetService)
        {
            _logger = logger;
            _bus = bus;
            _assetService = assetService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fake static engine daemon service started");

            var assets = (await _assetService.GetAssetsByFilterAsync(new GetAssetsByFilterRequest
            {
                Filter = new AssetBaseFilterDto
                {
                    Exchange = new ExchangeFilterDto
                    {
                        EngineType = EngineTypeDto.FAKE
                    },
                    Count = 1000
                }
            })).Assets;

            foreach (var asset in assets)
            {
                var provider = new GaussianRandomOhlcProvider(asset.Id, 5000);
                provider.OnPriceChanged += RaiseOhlcTimeseriesChangedAsync;
                
                _ohlcProviders.Add(provider);
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fake static engine daemon service stopped");
        }

        public async Task RaiseOhlcTimeseriesChangedAsync(IOhlcProvider sender, OhlcProviderEventArgs args)
        {
            await _bus.PublishAsync<OnOhlcTimeseriesChanged>(new OnOhlcTimeseriesChanged
            {
                AssetId = sender.AssetId,
                OhlcByInterval = args.OhlcByInterval,
                LastUpdate = DateTime.Now
            }, sender.AssetId.ToString());
        }
    }
}