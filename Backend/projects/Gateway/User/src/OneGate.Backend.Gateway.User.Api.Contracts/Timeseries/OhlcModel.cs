using System;
using Newtonsoft.Json;

namespace OneGate.Backend.Gateway.User.Api.Contracts.Timeseries
{
    public class OhlcModel
    {
        [JsonProperty("open")]
        public double Open { get; set; }
        
        [JsonProperty("high")]
        public double High { get; set; }
        
        [JsonProperty("low")]
        public double Low { get; set; }

        [JsonProperty("close")]
        public double Close { get; set; }
        
        [JsonProperty("volume")]
        public double Volume { get; set; }
        
        [JsonProperty("timestamp")] 
        public DateTime Timestamp { get; set; }
    }
}