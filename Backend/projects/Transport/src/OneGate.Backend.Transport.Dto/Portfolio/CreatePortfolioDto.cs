using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Dto.Portfolio
{
    public class CreatePortfolioDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}