using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.Models.Order
{
    public class StopOrderDto: OrderBaseDto
    {
        public override OrderTypeDto Type { get; } = OrderTypeDto.STOP;
        
        [JsonProperty("price")] 
        [Required] 
        public float Price { get; set; }
    }
}