﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.Models.Timeseries
{
    public class ValueTimeseriesRangeDto
    {
        [Required] 
        [JsonProperty("name")]
        public string Name { get; set; }

        [Required]
        [JsonProperty("asset_id")]
        public int AssetId { get; set; }
        
        [Required]
        [JsonProperty("range")]
        public List<ValueTimeseriesDto> Range { get; set; }
    }
}