using MassTransit.Topology;
using Newtonsoft.Json;

namespace OneGate.Backend.Core.Base.Contracts
{
    [EntityName("response.created_resource")]
    public class CreatedResourceResponse
    {
        [JsonProperty("id")]
        public int? Id { get; set; }
    }
}