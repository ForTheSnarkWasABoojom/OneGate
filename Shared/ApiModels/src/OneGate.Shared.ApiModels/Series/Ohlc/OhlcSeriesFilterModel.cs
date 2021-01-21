using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.Series.Ohlc
{
    public class OhlcSeriesFilterModel : SeriesFilterModel
    {
        [FromQuery(Name = "id")]
        [JsonProperty("id")]
        public int? Id { get; set; }
        
        [FromQuery(Name = "interval")]
        [Required]
        [JsonProperty("interval")]
        public IntervalModel Interval { get; set; }
    }
}