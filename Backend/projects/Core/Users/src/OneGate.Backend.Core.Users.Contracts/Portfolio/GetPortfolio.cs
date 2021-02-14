using MassTransit.Topology;
using Newtonsoft.Json;
using OneGate.Backend.Transport.Contracts;

namespace OneGate.Backend.Core.Users.Contracts.Portfolio
{
    [EntityName("request.portfolio.get")]
    public class GetPortfolios : LargeDataRequest
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("owner_id")]
        public int? OwnerId { get; set; }
    }
}