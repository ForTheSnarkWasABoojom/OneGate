using MassTransit.Topology;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Contracts.Timeseries
{
    [EntityName("request.value_timeseries.get")]
    public class GetValueTimeseries
    {
        public ValueTimeseriesFilterDto Filter { get; set; }
    }
}