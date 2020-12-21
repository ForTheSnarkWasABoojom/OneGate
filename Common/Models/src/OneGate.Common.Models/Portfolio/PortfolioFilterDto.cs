using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OneGate.Common.Models.Common;

namespace OneGate.Common.Models.Portfolio
{
    public class PortfolioFilterDto: FilterDto
    {
        [FromQuery(Name = "id")]
        [JsonProperty("id")]
        public int? Id { get; set; }

        [FromQuery(Name = "name")]
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}