using MassTransit.Topology;

namespace OneGate.Backend.Contracts.Asset
{
    [EntityName("request.asset.delete")]
    public class DeleteAsset
    {
        public int Id { get; set; }
    }
}