using MassTransit.Topology;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Contracts.Timeseries
{
    [EntityName("request.ohlc_timeseries.delete")]
    public class DeleteOhlcTimeseries
    {
        public OhlcTimeseriesFilterDto Filter { get; set; }
    }
}