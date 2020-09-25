using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace OneGate.Shared.Models.Timeseries
{
    public class ValueTimeseriesFilterDto : TimeseriesFilterBaseDto
    {
        [FromQuery(Name = "name")]
        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}