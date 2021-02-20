using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.User.Timeseries
{
    public class OhlcSeries : Series
    {
        public override SeriesType? Type => SeriesType.OHLC;
        
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