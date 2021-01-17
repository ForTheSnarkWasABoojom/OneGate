using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OneGate.Shared.ApiContracts.Common;

namespace OneGate.Shared.ApiContracts.Exchange
{
    public class ExchangeFilterDto : FilterDto
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