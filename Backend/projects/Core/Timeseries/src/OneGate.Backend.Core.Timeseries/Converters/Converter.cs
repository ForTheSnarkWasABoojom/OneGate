using OneGate.Backend.Core.Timeseries.Database.Models;
using OneGate.Backend.Transport.Dto.Series.Ohlc;
using OneGate.Backend.Transport.Dto.Series.Point;

namespace OneGate.Backend.Core.Timeseries.Converters
{
    public class Converter:IConverter
    {
        public PointDto ToDto(PointSeries src)
        {
            return new PointDto
            {
                Value = src.Value
            };
        }

        public PointSeries FromDto(PointDto src)
        {
            return new PointSeries
            {
                Value = src.Value
            };
        }

        public OhlcDto ToDto(OhlcSeries src)
        {
            return new OhlcDto
            {
                Low = src.Low,
                High = src.High,
                Open = src.Open,
                Close = src.Close
            };
        }

        public OhlcSeries FromDto(OhlcDto src)
        {
            return new OhlcSeries
            {
                Low = src.Low,
                High = src.High,
                Open = src.Open,
                Close = src.Close
            };
        }
    }
}