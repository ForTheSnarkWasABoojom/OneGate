using Newtonsoft.Json;
using OneGate.Backend.Transport.Dto.Common;

namespace OneGate.Backend.Transport.Dto.Portfolio
{
    public class PortfolioFilterDto: FilterDto
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}