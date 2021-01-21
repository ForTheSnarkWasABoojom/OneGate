using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.Order
{
    public class StopOrderModel : OrderModel
    {
        public override OrderTypeModel? Type { get; } = OrderTypeModel.STOP;

        [JsonProperty("price")] 
        [Required] 
        public float Price { get; set; }
    }
}