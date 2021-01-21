using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.Series.Point
{
    public class PointModel
    {
        [Required]
        [JsonProperty("value")]
        public float Value { get; set; }

        [Required]
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}