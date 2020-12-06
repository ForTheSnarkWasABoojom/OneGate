using MassTransit.Topology;
using OneGate.Shared.Models.Series.Point;

namespace OneGate.Backend.Contracts.Series.Point
{  
    [EntityName("request.point_series.create")]
    public class CreatePointSeries
    {
        public PointSeriesDto Series { get; set; }
    }
}