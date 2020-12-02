using MassTransit.Topology;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Contracts.OhlcTimeseries
{
    [EntityName("request.ohlc_timeseries.get")]
    public class GetOhlcTimeseries
    {
        public OhlcTimeseriesFilterDto Filter { get; set; }
    }
}