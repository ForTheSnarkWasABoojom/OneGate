using MassTransit.Topology;
using OneGate.Shared.Models.Series.Ohlc;

namespace OneGate.Backend.Contracts.Series.Ohlc
{
    [EntityName("request.ohlc_series.delete")]
    public class DeleteOhlcSeries
    {
        public OhlcSeriesFilterDto Filter { get; set; }
    }
}