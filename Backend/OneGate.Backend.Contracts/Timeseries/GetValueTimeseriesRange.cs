using MassTransit.Topology;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Contracts.Timeseries
{
    [EntityName("value_timeseries.get_range")]
    public class GetValueTimeseriesRange
    {
        public ValueTimeseriesFilterDto Filter { get; set; }
    }
}