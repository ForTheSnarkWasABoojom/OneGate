using System.Collections.Generic;
using MassTransit.Topology;
using Newtonsoft.Json;

namespace OneGate.Backend.Core.Assets.Contracts.Layer
{
    [EntityName("response.layers")]
    public class LayersResponse
    {
        [JsonProperty("layers")]
        public IEnumerable<LayersDto> Layers { get; set; }
    }
}