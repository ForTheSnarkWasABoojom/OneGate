using MassTransit.Topology;
using OneGate.Backend.Transport.Dto.Series.Point;

namespace OneGate.Backend.Transport.Contracts.Series.Point
{
    [EntityName("request.point_series.create")]
    public class CreatePointSeries
    {
        public PointSeriesDto Series { get; set; }
    }
}