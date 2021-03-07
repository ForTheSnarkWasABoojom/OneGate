using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Backend.Core.Timeseries.Api.Contracts.Layer;
using OneGate.Backend.Core.Timeseries.Api.Contracts.Ohlc;

namespace OneGate.Backend.Core.Timeseries.Api.Client
{
    public interface ITimeseriesApiClient
    {
        public Task<IEnumerable<OhlcDto>> GetOhlcsAsync(FilterOhlcDto request);
    }
}