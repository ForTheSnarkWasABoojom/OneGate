using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneGate.Backend.Core.Assets.Contracts.Asset
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AssetTypeDto
    {
        STOCK,
        INDEX
    }
}