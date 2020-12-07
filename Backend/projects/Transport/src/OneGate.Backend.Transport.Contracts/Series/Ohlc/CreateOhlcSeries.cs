using MassTransit.Topology;
using OneGate.Common.Models.Series.Ohlc;

namespace OneGate.Backend.Transport.Contracts.Series.Ohlc
{
    [EntityName("request.ohlc_series.create")]
    public class CreateOhlcSeries
    {
        public OhlcSeriesDto Series { get; set; }
    }
}