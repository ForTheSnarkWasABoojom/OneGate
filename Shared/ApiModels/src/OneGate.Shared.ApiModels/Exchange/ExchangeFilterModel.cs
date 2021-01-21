using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OneGate.Shared.ApiModels.Common;

namespace OneGate.Shared.ApiModels.Exchange
{
    public class ExchangeFilterModel : FilterModel
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
        public EngineTypeModel? EngineType { get; set; }
    }
}