using OneGate.Backend.Rpc.Contracts.Base;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Rpc.Contracts.Timeseries.CreateOhlcTimeseries
{
    public class CreateOhlcTimeseriesResponse : SuccessResponse
    {
        public OhlcTimeseriesRangeDto OhlcRange { get; set; }
    }
}