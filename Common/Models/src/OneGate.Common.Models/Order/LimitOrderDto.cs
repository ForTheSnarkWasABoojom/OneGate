using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Common.Models.Order
{
    public class LimitOrderDto : OrderDto
    {
        public override OrderTypeDto? Type => OrderTypeDto.LIMIT;
        
        [JsonProperty("price")] 
        [Required] 
        public float Price { get; set; }
    }
}