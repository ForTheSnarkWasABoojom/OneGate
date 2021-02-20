using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneGate.Backend.Core.Timeseries.Api.Contracts.Series
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SeriesTypeDto
    {
        OHLC,
        POINT
    }
}