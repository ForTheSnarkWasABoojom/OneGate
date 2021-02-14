﻿using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.User.Order.Limit
{
    public class LimitOrderModel : OrderModel
    {
        public override OrderTypeModel? Type => OrderTypeModel.LIMIT;
        
        [JsonProperty("price")] 
        [Required] 
        public float Price { get; set; }
    }
}