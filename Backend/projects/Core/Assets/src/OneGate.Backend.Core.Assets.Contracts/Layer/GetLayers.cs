using MassTransit.Topology;
using Newtonsoft.Json;
using OneGate.Backend.Transport.Contracts;

namespace OneGate.Backend.Core.Assets.Contracts.Layer
{
    [EntityName("request.layers.get")]
    public class GetLayers : LargeDataRequest
    {
        [JsonProperty("id")]
        public int? Id { get; set; }
    }
}