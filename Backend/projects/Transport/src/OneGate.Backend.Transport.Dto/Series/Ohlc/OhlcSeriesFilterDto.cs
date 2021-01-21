using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Dto.Series.Ohlc
{
    public class OhlcSeriesFilterDto : SeriesFilterDto
    {
        [JsonProperty("id")]
        public int? Id { get; set; }
        
        [JsonProperty("interval")]
        public IntervalDto Interval { get; set; }
    }
}