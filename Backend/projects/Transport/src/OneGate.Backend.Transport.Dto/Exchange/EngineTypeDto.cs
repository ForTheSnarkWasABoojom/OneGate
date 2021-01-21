using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneGate.Backend.Transport.Dto.Exchange
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EngineTypeDto
    {
        FAKE
    }
}