using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiContracts.Portfolio
{
    public class CreatePortfolioDto
    {
        [Required]
        [MaxLength(50)]
        [JsonProperty("name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}