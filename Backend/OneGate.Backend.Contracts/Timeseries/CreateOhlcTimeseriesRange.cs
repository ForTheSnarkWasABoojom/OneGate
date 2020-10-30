using MassTransit.Topology;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Contracts.Timeseries
{
    [EntityName("ohlc_timeseries.create_range")]
    public class CreateOhlcTimeseriesRange
    {
        public OhlcTimeseriesRangeDto Ohlcs { get; set; }
    }
}