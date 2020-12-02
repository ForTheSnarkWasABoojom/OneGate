using MassTransit.Topology;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Contracts.ValueTimeseries
{
    [EntityName("response.value_timeseries")]
    public class ValueTimeseriesResponse
    {
        public ValueTimeseriesRangeDto Values { get; set; }
    }
}