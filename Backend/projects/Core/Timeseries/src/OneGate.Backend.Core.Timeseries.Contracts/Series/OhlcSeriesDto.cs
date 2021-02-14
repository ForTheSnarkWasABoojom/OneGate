using Newtonsoft.Json;

namespace OneGate.Backend.Core.Timeseries.Contracts.Series
{
    public class OhlcSeriesDto : SeriesDto
    {
        public override SeriesTypeDto? Type => SeriesTypeDto.OHLC;
        
        [JsonProperty("low")]
        public double Low { get; set; }

        [JsonProperty("high")]
        public double High { get; set; }

        [JsonProperty("open")]
        public double Open { get; set; }

        [JsonProperty("close")]
        public double Close { get; set; }
    }
}