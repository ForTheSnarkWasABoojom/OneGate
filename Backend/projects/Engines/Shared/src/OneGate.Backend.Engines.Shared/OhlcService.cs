using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Core.Engines.Api.Client;
using OneGate.Backend.Core.Engines.Api.Contracts.AssetMapping;

namespace OneGate.Backend.Engines.Shared
{
    public abstract class OhlcService : IHostedService
    {
        public virtual int EngineId { get; } = 0;
        private readonly IEnginesApiClient _enginesApi;
        
        public OhlcService(IEnginesApiClient enginesApi)
        {
            _enginesApi = enginesApi;
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var assetMappings = await _enginesApi.GetAssetMappingsAsync(new FilterAssetMappingsDto
            {
                EngineId = EngineId
            });
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
        }
    }
}