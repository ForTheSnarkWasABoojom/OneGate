using OneGate.Shared.Models.Asset;

namespace OneGate.Backend.Rpc.Contracts.Asset.CreateAsset
{
    public class CreateAssetRequest
    {
        public CreateAssetBaseDto Asset { get; set; }
    }
}