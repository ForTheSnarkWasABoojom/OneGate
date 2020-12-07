using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Common.Models.Asset
{
    public class CreateStockAssetDto : CreateAssetDto
    {
        public override AssetTypeDto? Type { get; } = AssetTypeDto.STOCK;
        
        [MaxLength(30)]
        [JsonProperty("company")]
        public string Company { get; set; }
    }
}