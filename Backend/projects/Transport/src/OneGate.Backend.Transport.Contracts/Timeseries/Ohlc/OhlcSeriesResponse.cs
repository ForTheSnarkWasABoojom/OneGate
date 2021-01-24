using MassTransit.Topology;
using OneGate.Backend.Transport.Dto.Series.Ohlc;

namespace OneGate.Backend.Transport.Contracts.Timeseries.Ohlc
{
    [EntityName("response.ohlc_series")]
    public class OhlcSeriesResponse
    {
        public OhlcSeriesDto Series { get; set; }
    }
}