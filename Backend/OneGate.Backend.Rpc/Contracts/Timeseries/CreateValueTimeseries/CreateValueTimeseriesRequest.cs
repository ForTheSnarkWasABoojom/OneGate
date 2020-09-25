using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Rpc.Contracts.Timeseries.CreateValueTimeseries
{
    public class CreateValueTimeseriesRequest
    {
        public CreateValueTimeseriesRangeDto ValueTimeseriesRange { get; set; }
    }
}