using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OneGate.Backend.Core.Engines.Api.Contracts.AssetMapping;

namespace OneGate.Backend.Core.Engines.Api.Client
{
    public interface IEnginesApiClient
    {
        public Task<IEnumerable<AssetMappingDto>> GetAssetMappingsAsync(FilterAssetMappingsDto request,
            CancellationToken cancellationToken = default);
    }
}