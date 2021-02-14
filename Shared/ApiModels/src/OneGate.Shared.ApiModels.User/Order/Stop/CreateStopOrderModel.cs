using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.User.Order.Stop
{
    public class CreateStopOrderModel : CreateOrderModel
    {
        public override OrderTypeModel? Type => OrderTypeModel.STOP;

        [JsonProperty("price")] 
        [Required] 
        public float Price { get; set; }
    }
}