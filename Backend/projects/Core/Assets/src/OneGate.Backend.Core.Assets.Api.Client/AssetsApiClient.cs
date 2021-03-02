using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using OneGate.Backend.Core.Assets.Api.Contracts.Asset;
using OneGate.Backend.Core.Assets.Api.Contracts.Exchange;
using OneGate.Backend.Core.Shared.Api.Client;

namespace OneGate.Backend.Core.Assets.Api.Client
{
    public class AssetsApiClient : IAssetsApiClient
    {
        private readonly Uri _baseUrl;

        public AssetsApiClient(IOptions<AssetsApiClientOptions> options)
        {
            _baseUrl = options.Value.BaseUri;
        }

        public async Task<IEnumerable<AssetDto>> GetAssetsAsync(FilterAssetsDto request)
        {
            var result = await _baseUrl
                .AppendPathSegment("assets")
                .SetQueryParamsFromModel(request)
                .GetJsonAsync<IEnumerable<AssetDto>>();

            return result;
        }

        public async Task<IEnumerable<ExchangeDto>> GetExchangesAsync(FilterExchangesDto request)
        {
            var result = await _baseUrl
                .AppendPathSegment("exchanges")
                .SetQueryParamsFromModel(request)
                .GetJsonAsync<IEnumerable<ExchangeDto>>();

            return result;
        }
    }
}