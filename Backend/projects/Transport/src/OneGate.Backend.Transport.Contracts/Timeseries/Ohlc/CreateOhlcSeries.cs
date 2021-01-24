using MassTransit.Topology;
using OneGate.Backend.Transport.Dto.Series.Ohlc;

namespace OneGate.Backend.Transport.Contracts.Timeseries.Ohlc
{
    [EntityName("request.ohlc_series.create")]
    public class CreateOhlcSeries
    {
        public OhlcSeriesDto Series { get; set; }
    }
}