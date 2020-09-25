using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Rpc.Contracts.Timeseries.GetValueTimeseriesByFilter
{
    public class GetValueTimeseriesByFilterRequest
    {
        public ValueTimeseriesFilterDto Filter { get; set; }
    }
}