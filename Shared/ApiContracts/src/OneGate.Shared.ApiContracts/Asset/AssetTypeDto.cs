using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneGate.Shared.ApiContracts.Asset
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AssetTypeDto
    {
        STOCK,
        INDEX
    }
}