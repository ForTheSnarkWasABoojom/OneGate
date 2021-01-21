using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.Asset
{
    public class CreateIndexAssetModel : CreateAssetModel
    {
        public override AssetTypeModel? Type { get; } = AssetTypeModel.INDEX;

        [MaxLength(30)]
        [JsonProperty("country")]
        public string Country { get; set; }
    }
}