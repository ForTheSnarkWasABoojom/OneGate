using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneGate.Backend.Gateway.User.Api.Contracts.Order
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderType
    {
        MARKET,
        LIMIT,
        STOP
    }
}