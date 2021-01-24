using MassTransit.Topology;
using OneGate.Backend.Transport.Dto.Series.Ohlc;

namespace OneGate.Backend.Transport.Contracts.Timeseries.Ohlc
{
    [EntityName("request.ohlc_series.delete")]
    public class DeleteOhlcSeries
    {
        public OhlcSeriesFilterDto Filter { get; set; }
    }
}