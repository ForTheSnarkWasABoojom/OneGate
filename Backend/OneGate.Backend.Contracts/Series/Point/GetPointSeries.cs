using MassTransit.Topology;
using OneGate.Shared.Models.Series.Point;

namespace OneGate.Backend.Contracts.Series.Point
{
    [EntityName("request.point_series.get")]
    public class GetPointSeries
    {
        public PointSeriesFilterDto Filter { get; set; }
    }
}