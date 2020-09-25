using OneGate.Backend.Rpc.Contracts.Base;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Rpc.Contracts.Timeseries.GetOhlcTimeseriesByFilter
{
    public class GetOhlcTimeseriesByFilterResponse : SuccessResponse
    {
        public OhlcTimeseriesRangeDto OhlcRange { get; set; }
    }
}