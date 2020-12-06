using MassTransit.Topology;
using OneGate.Shared.Models.Series.Ohlc;

namespace OneGate.Backend.Contracts.Series.Ohlc
{
    [EntityName("response.ohlc_series")]
    public class OhlcSeriesResponse
    {
        public OhlcSeriesDto Series { get; set; }
    }
}