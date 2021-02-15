using MassTransit.Topology;
using Newtonsoft.Json;
using OneGate.Backend.Core.Base.Contracts;

namespace OneGate.Backend.Core.Assets.Contracts.Asset
{
    [EntityName("request.asset.get")]
    public class GetAssets : LargeDataRequest
    {
        [JsonProperty("id")]
        public int? Id { get; set; }
        
        [JsonProperty("ticker")]
        public string Ticker { get; set; }
        
        [JsonProperty("exchange_id")]
        public int? ExchangeId { get; set; }
    }
}