using MassTransit.Topology;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Contracts.Timeseries
{
    [EntityName("ohlc_timeseries.get_range")]
    public class GetOhlcTimeseriesRange
    {
        public OhlcTimeseriesFilterDto Filter { get; set; }
    }
}