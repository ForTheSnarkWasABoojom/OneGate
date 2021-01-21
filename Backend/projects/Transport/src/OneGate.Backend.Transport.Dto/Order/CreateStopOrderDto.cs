using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Dto.Order
{
    public class CreateStopOrderDto:CreateOrderDto
    {
         public override OrderTypeDto? Type => OrderTypeDto.STOP;
         
         [JsonProperty("price")]
         public float Price { get; set; }
    }
}