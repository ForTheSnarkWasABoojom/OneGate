﻿using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiContracts.Order
{
    public class StopOrderDto : OrderDto
    {
        public override OrderTypeDto? Type { get; } = OrderTypeDto.STOP;

        [JsonProperty("price")] 
        [Required] 
        public float Price { get; set; }
    }
}