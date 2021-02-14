using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneGate.Backend.Core.Assets.Contracts.Exchange
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EngineTypeDto
    {
        FAKE
    }
}