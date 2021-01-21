using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Dto.Exchange
{
    public class CreateExchangeDto
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("engine")]
        public EngineTypeDto? EngineType { get; set; }
    }
}