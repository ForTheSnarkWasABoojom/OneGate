using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.Models.Ohlc
{
    public class OhlcDto
    {
        [Required]
        [JsonProperty("low")]
        public float Low { get; set; }

        [Required]
        [JsonProperty("high")]
        public float High { get; set; }

        [Required]
        [JsonProperty("open")]
        public float Open { get; set; }

        [Required]
        [JsonProperty("close")]
        public float Close { get; set; }

        [Required]
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}