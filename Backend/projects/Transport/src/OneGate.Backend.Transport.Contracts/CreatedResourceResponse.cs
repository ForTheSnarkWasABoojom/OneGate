using MassTransit.Topology;
using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Contracts
{
    [EntityName("response.created_resource")]
    public class CreatedResourceResponse
    {
        [JsonProperty("id")]
        public int? Id { get; set; }
    }
}