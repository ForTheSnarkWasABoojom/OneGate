using MassTransit.Topology;
using Newtonsoft.Json;
using OneGate.Backend.Core.Base.Contracts;

namespace OneGate.Backend.Core.Users.Contracts.Order
{
    [EntityName("request.order.get")]
    public class GetOrders : LargeDataRequest
    {
        [JsonProperty("id")]
        public int? Id { get; set; }
        
        [JsonProperty("owner_id")]
        public int? OwnerId { get; set; }
    }
}