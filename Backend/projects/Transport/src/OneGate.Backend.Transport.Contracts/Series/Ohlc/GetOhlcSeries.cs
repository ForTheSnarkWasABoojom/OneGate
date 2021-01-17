using MassTransit.Topology;
using OneGate.Shared.ApiContracts.Series.Ohlc;

namespace OneGate.Backend.Transport.Contracts.Series.Ohlc
{
    [EntityName("request.ohlc_series.get")]
    public class GetOhlcSeries
    {
        public OhlcSeriesFilterDto Filter { get; set; }
    }
}