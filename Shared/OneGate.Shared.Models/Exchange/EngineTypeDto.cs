using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneGate.Shared.Models.Exchange
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EngineTypeDto
    {
        FAKE
    }
}