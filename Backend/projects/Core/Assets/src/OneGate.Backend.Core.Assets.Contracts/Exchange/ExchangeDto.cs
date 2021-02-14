using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Backend.Core.Assets.Contracts.Exchange
{
    public class ExchangeDto
    {
        
        [JsonProperty("id")]
        public int Id { get; set; }
        
        
        [MaxLength(50)]
        [JsonProperty("title")]
        public string Title { get; set; }
        
        
        [MaxLength(500)]
        [JsonProperty("description")]
        public string Description { get; set; }
        
        
        [MaxLength(150)]
        [JsonProperty("website")]
        public string Website { get; set; }
        
        
        [JsonProperty("engine_type")]
        public EngineTypeDto EngineType { get; set; }
    }
}