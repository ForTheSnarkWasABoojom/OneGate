using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.Asset
{
    public class StockAssetModel : AssetModel
    {
        public override AssetTypeModel? Type => AssetTypeModel.STOCK;

        [MaxLength(30)]
        [JsonProperty("company")]
        public string Company { get; set; }
    }
}