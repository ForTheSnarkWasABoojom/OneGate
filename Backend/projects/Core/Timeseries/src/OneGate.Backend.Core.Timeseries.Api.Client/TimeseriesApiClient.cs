using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using OneGate.Backend.Core.Shared.Api.Client;
using OneGate.Backend.Core.Timeseries.Api.Contracts.Layer;
using OneGate.Backend.Core.Timeseries.Api.Contracts.Ohlc;

namespace OneGate.Backend.Core.Timeseries.Api.Client
{
    public class TimeseriesApiClient : ITimeseriesApiClient
    {
        private readonly Uri _baseUrl;

        public TimeseriesApiClient(IOptions<TimeseriesApiClientOptions> options)
        {
            _baseUrl = options.Value.BaseUri;
        }

        public async Task<IEnumerable<OhlcDto>> GetOhlcsAsync(FilterOhlcDto request)
        {
            var result = await _baseUrl
                .AppendPathSegment("ohlcs")
                .SetQueryParamsFromModel(request)
                .GetJsonAsync<IEnumerable<OhlcDto>>();

            return result;
        }
    }
}