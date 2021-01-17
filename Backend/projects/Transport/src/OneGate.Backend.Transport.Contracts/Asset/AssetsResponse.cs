using System.Collections.Generic;
using MassTransit.Topology;
using OneGate.Shared.ApiContracts.Asset;

namespace OneGate.Backend.Transport.Contracts.Asset
{
    [EntityName("response.assets")]
    public class AssetsResponse
    {
        public IEnumerable<AssetDto> Assets { get; set; }
    }
}