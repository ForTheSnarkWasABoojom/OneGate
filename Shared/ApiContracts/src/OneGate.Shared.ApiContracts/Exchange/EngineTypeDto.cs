using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneGate.Shared.ApiContracts.Exchange
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EngineTypeDto
    {
        FAKE
    }
}