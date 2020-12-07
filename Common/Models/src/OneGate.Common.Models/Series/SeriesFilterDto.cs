using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OneGate.Common.Models.Common;

namespace OneGate.Common.Models.Series
{
    public class SeriesFilterDto : FilterDto
    {
        [FromQuery(Name = "asset_id")]
        [Required]
        [JsonProperty("asset_id")]
        public int AssetId { get; set; }

        [FromQuery(Name = "end_timestamp")]
        [JsonProperty("end_timestamp")]
        public DateTime? EndTimestamp { get; set; }

        [FromQuery(Name = "start_timestamp")]
        [JsonProperty("start_timestamp")]
        public DateTime? StartTimestamp { get; set; }
    }
}