using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.Asset
{
    public class CreateStockAssetModel : CreateAssetModel
    {
        public override AssetTypeModel? Type { get; } = AssetTypeModel.STOCK;
        
        [MaxLength(30)]
        [JsonProperty("company")]
        public string Company { get; set; }
    }
}