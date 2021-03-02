using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Backend.Gateway.User.Api.Contracts.Asset
{
    public class IndexAssetModel : AssetModel
    {
        public override AssetType? Type => AssetType.INDEX;

        [MaxLength(30)]
        [JsonProperty("country")]
        public string Country { get; set; }
    }
}