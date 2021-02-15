using MassTransit.Topology;
using Newtonsoft.Json;
using OneGate.Backend.Core.Base.Contracts;

namespace OneGate.Backend.Core.Assets.Contracts.Exchange
{
    [EntityName("request.exchange.get")]
    public class GetExchanges : LargeDataRequest
    {
        [JsonProperty("id")]
        public int? Id { get; set; }
        
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}