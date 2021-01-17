using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiContracts.Asset
{
    public class CreateStockAssetDto : CreateAssetDto
    {
        public override AssetTypeDto? Type { get; } = AssetTypeDto.STOCK;
        
        [MaxLength(30)]
        [JsonProperty("company")]
        public string Company { get; set; }
    }
}