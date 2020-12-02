using MassTransit.Topology;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Contracts.ValueTimeseries
{  
    [EntityName("request.value_timeseries.create")]
    public class CreateValueTimeseries
    {
        public ValueTimeseriesRangeDto Values { get; set; }
    }
}