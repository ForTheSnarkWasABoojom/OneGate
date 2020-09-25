using OneGate.Backend.Rpc.Contracts.Base;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Rpc.Contracts.Timeseries.CreateValueTimeseries
{
    public class CreateValueTimeseriesResponse:SuccessResponse
    {
        public ValueTimeseriesRangeDto ValueTimeseriesRange { get; set; }
    }
}