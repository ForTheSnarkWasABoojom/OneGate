using MassTransit.Topology;
using OneGate.Shared.Models.Asset;

namespace OneGate.Backend.Contracts.Asset
{
    [EntityName("request.asset.get")]
    public class GetAssets
    {
        public AssetFilterDto Filter { get; set; }
    }
}