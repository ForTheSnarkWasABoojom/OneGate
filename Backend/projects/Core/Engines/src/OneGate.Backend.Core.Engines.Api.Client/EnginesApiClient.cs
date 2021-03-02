using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using OneGate.Backend.Core.Engines.Api.Contracts.AssetMapping;
using OneGate.Backend.Core.Shared.Api.Client;

namespace OneGate.Backend.Core.Engines.Api.Client
{
    public class EnginesApiClient : IEnginesApiClient
    {
        private readonly Uri _baseUrl;

        public EnginesApiClient(IOptions<EnginesApiClientOptions> options)
        {
            _baseUrl = options.Value.BaseUri;
        }

        public async Task<IEnumerable<AssetMappingDto>> GetAssetMappingsAsync(FilterAssetMappingsDto request, CancellationToken cancellationToken = default)
        {
            var result = await _baseUrl
                .AppendPathSegment("asset_mappings")
                .SetQueryParamsFromModel(request)
                .GetJsonAsync<IEnumerable<AssetMappingDto>>(cancellationToken);

            return result;
        }
    }
}