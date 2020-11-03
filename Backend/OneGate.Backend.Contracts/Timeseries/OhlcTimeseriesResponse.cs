using MassTransit.Topology;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Contracts.Timeseries
{
    [EntityName("response.ohlc_timeseries")]
    public class OhlcTimeseriesResponse
    {
        public OhlcTimeseriesRangeDto Ohlcs { get; set; }
    }
}