using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.Models.Asset
{
    public class StockDto : AssetBaseDto
    {
        public override AssetTypeDto Type { get; } = AssetTypeDto.STOCK;
        
        [MaxLength(30)]
        [JsonProperty("company")]
        public string Company { get; set; }
    }
}