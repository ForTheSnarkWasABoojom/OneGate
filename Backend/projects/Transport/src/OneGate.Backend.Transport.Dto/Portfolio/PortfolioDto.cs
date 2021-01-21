using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Dto.Portfolio
{
    public class PortfolioDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("owner_id")]
        public int OwnerId { get; set; }
    }
}