using MassTransit.Topology;
using Newtonsoft.Json;

namespace OneGate.Backend.Core.Users.Contracts.Portfolio
{
    [EntityName("request.portfolio.delete")]
    public class DeletePortfolio
    {
        [JsonProperty("id")]
        public int? Id { get; set; }
        
        [JsonProperty("owner_id")]
        public int? OwnerId { get; set; }
    }
}