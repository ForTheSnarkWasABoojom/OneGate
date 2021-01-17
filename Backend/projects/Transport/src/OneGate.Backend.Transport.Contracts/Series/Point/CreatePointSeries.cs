using MassTransit.Topology;
using OneGate.Shared.ApiContracts.Series.Point;

namespace OneGate.Backend.Transport.Contracts.Series.Point
{
    [EntityName("request.point_series.create")]
    public class CreatePointSeries
    {
        public PointSeriesDto Series { get; set; }
    }
}