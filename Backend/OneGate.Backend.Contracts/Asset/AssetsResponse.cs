using System.Collections.Generic;
using MassTransit.Topology;
using OneGate.Shared.Models.Asset;

namespace OneGate.Backend.Contracts.Asset
{
    [EntityName("response.assets")]
    public class AssetsResponse
    {
        public IEnumerable<AssetBaseDto> Assets { get; set; }
    }
}