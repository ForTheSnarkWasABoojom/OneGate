using MassTransit.Topology;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Contracts.Timeseries
{
    [EntityName("value_timeseries.delete_range")]
    public class DeleteValueTimeseriesRange
    {
        public ValueTimeseriesFilterDto Filter { get; set; }
    }
}