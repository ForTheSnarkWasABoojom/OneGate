using MassTransit.Topology;
using OneGate.Shared.ApiContracts.Asset;

namespace OneGate.Backend.Transport.Contracts.Asset
{
    [EntityName("request.asset.get")]
    public class GetAssets
    {
        public AssetFilterDto Filter { get; set; }
    }
}