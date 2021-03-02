using System;
using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Contracts.Timeseries
{
    public class OhlcSeriesDto
    {
        [JsonProperty("interval")]
        public SeriesIntervalDto Interval { get; set; }
        
        [JsonProperty("open")]
        public float Open { get; set; }
        
        [JsonProperty("high")]
        public float High { get; set; }

        [JsonProperty("low")]
        public float Low { get; set; }
        
        [JsonProperty("close")]
        public float Close { get; set; }
        
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}