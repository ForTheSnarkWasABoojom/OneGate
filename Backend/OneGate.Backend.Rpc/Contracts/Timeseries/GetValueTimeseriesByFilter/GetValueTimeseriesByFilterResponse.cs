using OneGate.Backend.Rpc.Contracts.Base;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Rpc.Contracts.Timeseries.GetValueTimeseriesByFilter
{
    public class GetValueTimeseriesByFilterResponse : SuccessResponse
    {
        public ValueTimeseriesRangeDto ValueTimeseriesRange { get; set; }
    }
}