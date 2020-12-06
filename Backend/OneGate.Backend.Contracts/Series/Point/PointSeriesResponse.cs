using MassTransit.Topology;
using OneGate.Shared.Models.Series.Point;

namespace OneGate.Backend.Contracts.Series.Point
{
    [EntityName("response.point_series")]
    public class PointSeriesResponse
    {
        public PointSeriesDto Series { get; set; }
    }
}