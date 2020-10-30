using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.Models.Asset
{
    public class StockAssetDto : AssetBaseDto
    {
        public override AssetTypeDto? Type => AssetTypeDto.STOCK;

        [MaxLength(30)]
        [JsonProperty("company")]
        public string Company { get; set; }
    }
}