using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Dto.Asset
{
    public class StockAssetDto : AssetDto
    {
        public override AssetTypeDto? Type => AssetTypeDto.STOCK;

        [JsonProperty("company")]
        public string Company { get; set; }
    }
}