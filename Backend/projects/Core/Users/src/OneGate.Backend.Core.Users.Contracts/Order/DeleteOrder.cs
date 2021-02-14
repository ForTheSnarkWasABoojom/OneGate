using MassTransit.Topology;
using Newtonsoft.Json;

namespace OneGate.Backend.Core.Users.Contracts.Order
{
    [EntityName("request.order.delete")]
    public class DeleteOrder
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("owner_id")]
        public int OwnerId { get; set; }
    }
}