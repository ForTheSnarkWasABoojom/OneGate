using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Rpc.Contracts.Timeseries.DeleteValueTimeseries
{
    public class DeleteValueTimeseriesRequest
    {
        public ValueTimeseriesFilterDto Filter { get; set; }
    }
}