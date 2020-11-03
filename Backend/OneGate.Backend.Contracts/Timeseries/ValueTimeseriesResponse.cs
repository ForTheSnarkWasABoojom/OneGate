using MassTransit.Topology;
using OneGate.Backend.Contracts.Common;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Contracts.Timeseries
{
    [EntityName("response.value_timeseries")]
    public class ValueTimeseriesResponse
    {
        public ValueTimeseriesRangeDto Values { get; set; }
    }
}