using MassTransit.Topology;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Contracts.Timeseries
{  
    [EntityName("value_timeseries.create_range")]
    public class CreateValueTimeseriesRange
    {
        public ValueTimeseriesRangeDto Values { get; set; }
    }
}