using OneGate.Backend.Contracts.Common;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Contracts.Timeseries
{
    public class ValueTimeseriesRangeResponse : SuccessResponse
    {
        public ValueTimeseriesRangeDto Values { get; set; }
    }
}