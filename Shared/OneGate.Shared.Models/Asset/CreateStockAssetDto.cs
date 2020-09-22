using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.Models.Asset
{
    public class CreateStockAssetDto : CreateAssetBaseDto
    {
        public override AssetTypeDto Type { get; } = AssetTypeDto.STOCK;
        
        [MaxLength(30)]
        [JsonProperty("company")]
        public string Company { get; set; }
    }
}