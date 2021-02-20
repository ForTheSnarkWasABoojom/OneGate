using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.User.Asset
{
    public class IndexAsset : Asset
    {
        public override AssetType? Type => AssetType.INDEX;

        [MaxLength(30)]
        [JsonProperty("country")]
        public string Country { get; set; }
    }
}