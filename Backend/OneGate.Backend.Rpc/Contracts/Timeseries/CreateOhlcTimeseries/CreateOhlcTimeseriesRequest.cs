using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Rpc.Contracts.Timeseries.CreateOhlcTimeseries
{
    public class CreateOhlcTimeseriesRequest
    {
        public CreateOhlcTimeseriesRangeDto OhlcRange { get; set; }
    }
}