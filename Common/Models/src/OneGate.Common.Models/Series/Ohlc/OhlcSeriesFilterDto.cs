using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace OneGate.Common.Models.Series.Ohlc
{
    public class  OhlcSeriesFilterDto : SeriesFilterDto
    {
        [FromQuery(Name = "id")]
        [JsonProperty("id")]
        public int? Id { get; set; }
        
        [FromQuery(Name = "interval")]
        [Required]
        [JsonProperty("interval")]
        public IntervalDto Interval { get; set; }
    }
}