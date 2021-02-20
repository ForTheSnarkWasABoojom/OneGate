using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneGate.Backend.Core.Assets.Api.Contracts.Asset
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AssetTypeDto
    {
        STOCK,
        INDEX
    }
}