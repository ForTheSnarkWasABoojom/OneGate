using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneGate.Backend.Gateway.User.Api.Contracts.Series
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SeriesType
    {
        POINT,
        OHLC
    }
}