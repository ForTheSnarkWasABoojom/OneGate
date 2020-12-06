using MassTransit.Topology;
using OneGate.Shared.Models.Series.Ohlc;

namespace OneGate.Backend.Contracts.Series.Ohlc
{
    [EntityName("request.ohlc_series.get")]
    public class GetOhlcSeries
    {
        public OhlcSeriesFilterDto Filter { get; set; }
    }
}