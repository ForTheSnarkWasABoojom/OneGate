using MassTransit.Topology;
using OneGate.Backend.Transport.Dto.Series.Point;

namespace OneGate.Backend.Transport.Contracts.Timeseries.Point
{
    [EntityName("request.point_series.delete")]
    public class DeletePointSeries
    {
        public PointSeriesFilterDto Filter { get; set; }
    }
}