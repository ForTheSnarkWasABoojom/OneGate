using MassTransit.Topology;
using Newtonsoft.Json;
using OneGate.Backend.Transport.Contracts;

namespace OneGate.Backend.Core.Assets.Contracts.Asset
{
    [EntityName("request.asset.get")]
    public class GetAssets : LargeDataRequest
    {
        [JsonProperty("id")]
        public int? Id { get; set; }
    }
}