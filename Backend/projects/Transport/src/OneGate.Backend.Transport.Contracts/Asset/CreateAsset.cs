using MassTransit.Topology;
using OneGate.Backend.Transport.Dto.Asset;

namespace OneGate.Backend.Transport.Contracts.Asset
{
    [EntityName("request.asset.create")]
    public class CreateAsset
    {
        public CreateAssetDto Asset { get; set; }
    }
}