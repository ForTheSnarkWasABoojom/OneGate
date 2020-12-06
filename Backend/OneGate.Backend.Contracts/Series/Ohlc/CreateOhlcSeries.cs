using MassTransit.Topology;
using OneGate.Shared.Models.Series.Ohlc;

namespace OneGate.Backend.Contracts.Series.Ohlc
{
    [EntityName("request.ohlc_series.create")]
    public class CreateOhlcSeries
    {
        public OhlcSeriesDto Series { get; set; }
    }
}