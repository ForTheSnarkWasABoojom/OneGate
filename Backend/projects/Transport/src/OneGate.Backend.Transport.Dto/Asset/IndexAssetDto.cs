using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Dto.Asset
{
    public class IndexAssetDto : AssetDto
    {
        public override AssetTypeDto? Type => AssetTypeDto.INDEX;

        [JsonProperty("country")]
        public string Country { get; set; }
    }
}