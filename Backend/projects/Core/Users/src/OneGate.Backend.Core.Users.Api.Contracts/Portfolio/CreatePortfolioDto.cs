using Newtonsoft.Json;

namespace OneGate.Backend.Core.Users.Api.Contracts.Portfolio
{
    public class CreatePortfolioDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("owner_id")]
        public int OwnerId { get; set; }
    }
}