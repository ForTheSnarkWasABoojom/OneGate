using MassTransit.Topology;
using OneGate.Shared.Models.Asset;

namespace OneGate.Backend.Contracts.Asset
{
    [EntityName("asset.create")]
    public class CreateAsset
    {
        public CreateAssetBaseDto Asset { get; set; }
    }
}