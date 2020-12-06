using MassTransit.Topology;
using OneGate.Shared.Models.Asset;

namespace OneGate.Backend.Contracts.Asset
{
    [EntityName("request.asset.create")]
    public class CreateAsset
    {
        public CreateAssetDto Asset { get; set; }
    }
}