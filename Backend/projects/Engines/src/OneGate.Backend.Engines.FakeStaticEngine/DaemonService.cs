﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Engines.Base.OhlcProvider;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Asset;
using OneGate.Backend.Transport.Contracts.Series.Ohlc;
using OneGate.Common.Models.Asset;
using OneGate.Common.Models.Exchange;

namespace OneGate.Backend.Engines.FakeStaticEngine
{
    public class DaemonService : IHostedService
    {
        private readonly ILogger<DaemonService> _logger;

        private readonly IOgBus _bus;
        private readonly IPublishEndpoint _endpoint;

        private readonly List<IOhlcProvider> _ohlcProviders = new List<IOhlcProvider>();

        public DaemonService(ILogger<DaemonService> logger, IOgBus bus, IPublishEndpoint endpoint)
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
                Filter = new AssetFilterDto
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
                provider.OnPriceChanged += RaiseOhlcSeriesChangedAsync;

                _ohlcProviders.Add(provider);
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fake static engine daemon service stopped");
        }

        private async Task RaiseOhlcSeriesChangedAsync(IOhlcProvider sender, OhlcProviderEventArgs args)
        {
            await _endpoint.Publish(new OnOhlcSeriesUpdated
            {
                AssetId = sender.AssetId,
                Data = args.OhlcByInterval,
                LastUpdate = DateTime.Now
            });
        }
    }
}