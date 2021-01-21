using System;
using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Dto.Series.Point
{
    public class PointDto
    {
        [JsonProperty("value")]
        public float Value { get; set; }
        
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}