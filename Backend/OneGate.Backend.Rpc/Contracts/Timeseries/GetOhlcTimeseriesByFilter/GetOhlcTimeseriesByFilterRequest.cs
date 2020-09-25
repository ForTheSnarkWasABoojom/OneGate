using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Rpc.Contracts.Timeseries.GetOhlcTimeseriesByFilter
{
    public class GetOhlcTimeseriesByFilterRequest
    {
        public OhlcTimeseriesFilterDto Filter { get; set; }
    }
}