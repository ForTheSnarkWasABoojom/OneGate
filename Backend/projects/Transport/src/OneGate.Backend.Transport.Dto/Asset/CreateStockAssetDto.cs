using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Dto.Asset
{
    public class CreateStockAssetDto : CreateAssetDto
    {
        public override AssetTypeDto? Type { get; } = AssetTypeDto.STOCK;
        
        [JsonProperty("company")]
        public string Company { get; set; }
    }
}