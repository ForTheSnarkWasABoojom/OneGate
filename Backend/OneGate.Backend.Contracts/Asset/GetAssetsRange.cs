using MassTransit.Topology;
using OneGate.Shared.Models.Asset;

namespace OneGate.Backend.Contracts.Asset
{
    [EntityName("asset.get_range")]
    public class GetAssetsRange
    {
        public AssetBaseFilterDto Filter { get; set; }
    }
}