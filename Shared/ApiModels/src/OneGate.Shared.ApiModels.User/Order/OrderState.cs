using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneGate.Shared.ApiModels.User.Order
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderState
    {
        ACCEPTED, 
        CONFIRMED,
        COMPLETED
    }
}