using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Backend.Core.Assets.Api.Contracts.Asset
{
    public class IndexAssetDto: AssetDto
    {
        public override AssetTypeDto? Type => AssetTypeDto.INDEX;

        [MaxLength(30)]
        [JsonProperty("country")]
        public string Country { get; set; }
    }
}