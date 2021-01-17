using MassTransit.Topology;
using OneGate.Shared.ApiContracts.Series.Point;

namespace OneGate.Backend.Transport.Contracts.Series.Point
{
    [EntityName("request.point_series.get")]
    public class GetPointSeries
    {
        public PointSeriesFilterDto Filter { get; set; }
    }
}