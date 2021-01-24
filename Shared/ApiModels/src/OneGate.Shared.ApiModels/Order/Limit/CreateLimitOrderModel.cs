using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.Order.Limit
{
    public class CreateLimitOrderModel : CreateOrderModel
    {
        public override OrderTypeModel? Type => OrderTypeModel.LIMIT;
        
        [JsonProperty("price")] 
        [Required] 
        public float Price { get; set; }
    }
}