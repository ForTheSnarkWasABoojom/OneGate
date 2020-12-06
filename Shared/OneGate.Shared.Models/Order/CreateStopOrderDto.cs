﻿using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.Models.Order
{
    public class CreateStopOrderDto:CreateOrderDto
    {
         public override OrderTypeDto? Type => OrderTypeDto.STOP;
         
         [JsonProperty("price")] 
         [Required] 
         public float Price { get; set; }
    }
}