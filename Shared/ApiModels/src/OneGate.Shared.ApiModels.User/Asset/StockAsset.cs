using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.User.Asset
{
    public class StockAsset : Asset
    {
        public override AssetType? Type => AssetType.STOCK;

        [MaxLength(30)]
        [JsonProperty("company")]
        public string Company { get; set; }
    }
}