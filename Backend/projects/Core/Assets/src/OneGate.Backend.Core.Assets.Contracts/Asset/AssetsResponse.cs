using System.Collections.Generic;
using MassTransit.Topology;
using Newtonsoft.Json;

namespace OneGate.Backend.Core.Assets.Contracts.Asset
{
    [EntityName("response.assets")]
    public class AssetsResponse
    {
        [JsonProperty("assets")]
        public IEnumerable<AssetDto> Assets { get; set; }
    }
}