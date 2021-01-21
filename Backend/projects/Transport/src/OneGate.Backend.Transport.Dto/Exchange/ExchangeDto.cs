using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Dto.Exchange
{
    public class ExchangeDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("engine_type")]
        public EngineTypeDto EngineType { get; set; }
    }
}