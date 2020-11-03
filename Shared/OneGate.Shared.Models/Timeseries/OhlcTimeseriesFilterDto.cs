using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace OneGate.Shared.Models.Timeseries
{
    public class OhlcTimeseriesFilterDto : TimeseriesFilterBaseDto
    {
        [FromQuery(Name = "id")]
        [JsonProperty("id")]
        public int? Id { get; set; }
        
        [FromQuery(Name = "interval")]
        [Required]
        [JsonProperty("interval")]
        public OhlcIntervalDto Interval { get; set; }
    }
}