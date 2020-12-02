using MassTransit.Topology;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Contracts.OhlcTimeseries
{
    [EntityName("request.ohlc_timeseries.create")]
    public class CreateOhlcTimeseries
    {
        public OhlcTimeseriesRangeDto Ohlcs { get; set; }
    }
}