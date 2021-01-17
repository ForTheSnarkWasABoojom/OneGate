using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneGate.Shared.ApiContracts.Order
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderTypeDto
    {
        MARKET,
        LIMIT,
        STOP
    }
}