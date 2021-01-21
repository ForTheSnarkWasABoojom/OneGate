using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneGate.Backend.Transport.Dto.Order
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderSideDto
    {
        BUY,
        SELL
    }
}