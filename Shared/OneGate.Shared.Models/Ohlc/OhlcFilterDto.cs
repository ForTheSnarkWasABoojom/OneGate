using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using OneGate.Shared.Models.Common;

namespace OneGate.Shared.Models.Ohlc
{
    public class OhlcFilterDto : FilterBaseDto
    {
        [FromQuery(Name = "interval")]
        [Required]
        [JsonProperty("interval")] 
        public OhlcIntervalDto Interval { get; set; }

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