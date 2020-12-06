using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.Models.Asset
{
    public class CreateIndexAssetDto : CreateAssetDto
    {
        public override AssetTypeDto? Type { get; } = AssetTypeDto.INDEX;

        [MaxLength(30)]
        [JsonProperty("country")]
        public string Country { get; set; }
    }
}