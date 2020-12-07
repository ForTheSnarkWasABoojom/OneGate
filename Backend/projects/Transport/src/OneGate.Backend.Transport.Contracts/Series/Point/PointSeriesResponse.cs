using MassTransit.Topology;
using OneGate.Common.Models.Series.Point;

namespace OneGate.Backend.Transport.Contracts.Series.Point
{
    [EntityName("response.point_series")]
    public class PointSeriesResponse
    {
        public PointSeriesDto Series { get; set; }
    }
}