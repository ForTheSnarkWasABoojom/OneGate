using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Backend.Gateway.User.Api.Contracts.Asset
{
    public class StockAssetModel : AssetModel
    {
        public override AssetType? Type => AssetType.STOCK;

        [MaxLength(30)]
        [JsonProperty("company")]
        public string Company { get; set; }
    }
}