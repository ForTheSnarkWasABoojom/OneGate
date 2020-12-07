using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace OneGate.Common.Models.Series.Point
{
    public class PointSeriesFilterDto : SeriesFilterDto
    {
        [FromQuery(Name = "id")]
        [JsonProperty("id")]
        public int? Id { get; set; }
        
        [FromQuery(Name = "layout_id")]
        [JsonProperty("layout_id")]
        public int LayoutId { get; set; }
        
        [FromQuery(Name = "name")]
        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}