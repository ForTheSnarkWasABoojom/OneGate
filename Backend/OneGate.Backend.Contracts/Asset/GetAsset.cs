using MassTransit.Topology;

namespace OneGate.Backend.Contracts.Asset
{
    [EntityName("asset.get")]
    public class GetAsset
    {
        public int Id { get; set; }
    }
}