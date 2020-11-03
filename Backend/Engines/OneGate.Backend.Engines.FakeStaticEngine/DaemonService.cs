using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Engines.Base.OhlcProvider;
using OneGate.Backend.Contracts.Asset;
using OneGate.Backend.Contracts.Timeseries;
using OneGate.Backend.Rpc;
using OneGate.Shared.Models.Asset;
using OneGate.Shared.Models.Exchange;

namespace OneGate.Backend.Engines.FakeStaticEngine
{
    public class DaemonService : IHostedService
    {
        private readonly ILogger<DaemonService> _logger;

        private readonly IBus _bus;
        private readonly IPublishEndpoint _endpoint;

        private readonly List<IOhlcProvider> _ohlcProviders = new List<IOhlcProvider>();

        public DaemonService(ILogger<DaemonService> logger, IBus bus, IPublishEndpoint endpoint)
        {
            _logger = logger;
            _bus = bus;
            _endpoint = endpoint;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fake static engine daemon service started");

            var assets = (await _bus.Call<GetAssets, AssetsResponse>(new GetAssets
            {
                Filter = new AssetBaseFilterDto
                {
                    Exchange = new ExchangeFilterDto
                    {
                        EngineType = EngineTypeDto.FAKE
                    },
                    Count = 1000
                }
            }, RequestTimeout.After(m: 5))).Assets;

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

        private async Task RaiseOhlcTimeseriesChangedAsync(IOhlcProvider sender, OhlcProviderEventArgs args)
        {
            await _endpoint.Publish(new OnOhlcTimeseriesUpdated
            {
                AssetId = sender.AssetId,
                Ohlcs = args.OhlcByInterval,
                LastUpdate = DateTime.Now
            });
        }
    }
}