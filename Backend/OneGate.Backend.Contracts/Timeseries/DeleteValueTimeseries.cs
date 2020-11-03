using MassTransit.Topology;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Contracts.Timeseries
{
    [EntityName("request.value_timeseries.delete")]
    public class DeleteValueTimeseries
    {
        public ValueTimeseriesFilterDto Filter { get; set; }
    }
}