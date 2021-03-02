using Newtonsoft.Json;

namespace OneGate.Backend.Gateway.User.Api.Contracts.Series
{
    public class OhlcSeriesModel : SeriesModel
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