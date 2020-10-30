using OneGate.Backend.Contracts.Common;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Contracts.Timeseries
{
    public class OhlcTimeseriesRangeResponse : SuccessResponse
    {
        public OhlcTimeseriesRangeDto Ohlcs { get; set; }
    }
}