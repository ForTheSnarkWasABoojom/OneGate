using MassTransit.Topology;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Contracts.Timeseries
{
    [EntityName("ohlc_timeseries.delete_range")]
    public class DeleteOhlcTimeseriesRange
    {
        public OhlcTimeseriesFilterDto Filter { get; set; }
    }
}