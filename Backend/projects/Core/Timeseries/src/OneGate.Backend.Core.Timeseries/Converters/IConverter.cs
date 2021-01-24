using OneGate.Backend.Core.Timeseries.Database.Models;
using OneGate.Backend.Transport.Dto.Series.Ohlc;
using OneGate.Backend.Transport.Dto.Series.Point;

namespace OneGate.Backend.Core.Timeseries.Converters
{
    public interface IConverter
    {
        public PointDto ToDto(PointSeries src);
        public PointSeries FromDto(PointDto src);
        
        public OhlcDto ToDto(OhlcSeries src);
        public OhlcSeries FromDto(OhlcDto src);
    }
}