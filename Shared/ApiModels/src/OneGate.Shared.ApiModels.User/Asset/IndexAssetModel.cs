using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.User.Asset
{
    public class IndexAssetModel : AssetModel
    {
        public override AssetTypeModel? Type => AssetTypeModel.INDEX;

        [MaxLength(30)]
        [JsonProperty("country")]
        public string Country { get; set; }
    }
}