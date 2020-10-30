using MassTransit.Topology;

namespace OneGate.Backend.Contracts.Asset
{
    [EntityName("asset.delete")]
    public class DeleteAsset
    {
        public int Id { get; set; }
    }
}