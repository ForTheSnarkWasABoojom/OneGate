using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Rpc.Contracts.Timeseries.CreateOhlcTimeseries
{
    public class CreateOhlcTimeseriesRequest
    {
        public OhlcTimeseriesRangeDto OhlcRange { get; set; }
    }
}