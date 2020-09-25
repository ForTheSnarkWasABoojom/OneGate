using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.Models.Timeseries
{
    public class CreateValueTimeseriesDto
    {
        [Required] 
        [JsonProperty("value")]
        public float Value { get; set; }

        [Required]
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}