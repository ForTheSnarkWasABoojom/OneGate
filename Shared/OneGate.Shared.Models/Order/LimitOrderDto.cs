using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.Models.Order
{
    public class LimitOrderDto : OrderBaseDto
    {
        public override OrderTypeDto Type { get; } = OrderTypeDto.LIMIT;
        [JsonProperty("price")] 
        [Required] 
        public float Price { get; set; }
    }
}