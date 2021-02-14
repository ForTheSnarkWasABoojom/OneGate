﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneGate.Backend.Core.Users.Contracts.Order
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderSideDto
    {
        BUY,
        SELL
    }
}