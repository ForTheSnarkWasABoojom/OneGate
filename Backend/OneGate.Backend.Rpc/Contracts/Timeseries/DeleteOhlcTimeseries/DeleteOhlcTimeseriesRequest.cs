using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Rpc.Contracts.Timeseries.DeleteOhlcTimeseries
{
    public class DeleteOhlcTimeseriesRequest
    {
        public OhlcTimeseriesFilterDto Filter { get; set; }
    }
}