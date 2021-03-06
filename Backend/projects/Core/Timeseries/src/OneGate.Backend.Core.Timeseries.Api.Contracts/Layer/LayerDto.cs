﻿using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Backend.Core.Timeseries.Api.Contracts.Layer
{
    public class LayerDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [MaxLength(100)]
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [MaxLength(500)]
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}