using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Core.Engines.Api.Client;
using OneGate.Backend.Core.Engines.Api.Contracts.AssetMapping;
using OneGate.Backend.Engines.Shared.Domain;
using OneGate.Backend.Engines.Shared.Static.Domain;
using OneGate.Backend.Engines.Shared.Static.MarketProvider;
using OneGate.Backend.Transport.Contracts.Timeseries;

namespace OneGate.Backend.Engines.Shared.Static.HostedServices
{
    public class MarketService : IHostedService
    {
        private readonly IMarketProvider _provider;
        
        private readonly ILogger<MarketService> _logger;
        private readonly IMapper _mapper;

        private readonly IBus _bus;
        private readonly IEnginesApiClient _enginesApi;
        private readonly IEngineMetadata _metadata;

        private readonly CancellationTokenSource _cancellationTokenSource;

        public MarketService(IMarketProvider provider, IEnginesApiClient enginesApi, ILogger<MarketService> logger,
            IBus bus, IMapper mapper, IEngineMetadata metadata)
        {
            _enginesApi = enginesApi;
            _logger = logger;
            _provider = provider;
            _bus = bus;
            _mapper = mapper;
            _metadata = metadata;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Market service started");
            
            await RunProvider();
        }

        private async Task RunProvider()
        {
            var assetMappings = await GetAssetMappingsAsync();
            var configuration = new MarketProviderConfiguration
            {
                AssetMappings = assetMappings
            };

            _provider.MarketUpdated += OnMarketUpdated;
            await _provider.RunAsync(configuration, _cancellationTokenSource.Token);
        }

        private async Task OnMarketUpdated(MarketUpdatedArgs arg, CancellationToken cancellationToken)
        {
            await PublishSeriesAsync(arg.AssetId, arg.Ohlc, cancellationToken);
        }

        private async Task PublishSeriesAsync(int assetId, List<OhlcSeries> ohlcRange, CancellationToken cancellationToken)
        {
            var ohlcsDtos = _mapper.Map<List<OhlcSeriesDto>>(ohlcRange);
            await _bus.Publish(new MarketDataUpdated
            {
                AssetId = assetId,
                Ohlcs = ohlcsDtos
            }, cancellationToken);
        }

        private async Task<Dictionary<string, int>> GetAssetMappingsAsync()
        {
            var assetMappingDtos = await _enginesApi.GetAssetMappingsAsync(new FilterAssetMappingsDto
            {
                EngineId = _metadata.EngineId
            }, _cancellationTokenSource.Token);
            
            return assetMappingDtos
                .ToDictionary(p => p.OriginalSymbol, p => p.AssetId);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogWarning("Trying to stop market service gracefully..");
            _cancellationTokenSource.Cancel();
        }
    }
}