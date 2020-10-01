using OneGate.Backend.Rpc.Contracts.Base;
using OneGate.Shared.Models.Common;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Rpc.Contracts.Timeseries.CreateOhlcTimeseries
{
    public class CreateOhlcTimeseriesResponse : SuccessResponse
    {
        public CreatedResourceDto CreatedResource { get; set; }
    }
}