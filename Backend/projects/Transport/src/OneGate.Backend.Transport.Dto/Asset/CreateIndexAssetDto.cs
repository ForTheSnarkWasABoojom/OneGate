using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Dto.Asset
{
    public class CreateIndexAssetDto : CreateAssetDto
    {
        public override AssetTypeDto? Type { get; } = AssetTypeDto.INDEX;
        
        [JsonProperty("country")]
        public string Country { get; set; }
    }
}