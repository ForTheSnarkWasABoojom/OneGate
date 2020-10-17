using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using OneGate.Shared.Models.Common;

namespace OneGate.Shared.Models.Exchange
{
    public class ExchangeFilterDto : FilterBaseDto
    {
        [FromQuery(Name = "id")]
        [JsonProperty("id")]
        public int? Id { get; set; }
        
        [MaxLength(50)]
        [FromQuery(Name = "title")]
        [JsonProperty("title")]
        public string Title { get; set; }
        
        [FromQuery(Name = "engine_type")]
        [JsonProperty("engine_type")]
        public EngineTypeDto? EngineType { get; set; }
    }
}