﻿using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.Series.Ohlc
{
    public class OhlcModel
    {
        [Required]
        [JsonProperty("low")]
        public double Low { get; set; }

        [Required]
        [JsonProperty("high")]
        public double High { get; set; }

        [Required]
        [JsonProperty("open")]
        public double Open { get; set; }

        [Required]
        [JsonProperty("close")]
        public double Close { get; set; }

        [Required]
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}