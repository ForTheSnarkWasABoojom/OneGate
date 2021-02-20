﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneGate.Shared.ApiModels.User.Timeseries
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SeriesType
    {
        POINT,
        OHLC
    }
}