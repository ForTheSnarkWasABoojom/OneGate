using OneGate.Backend.Rpc.Contracts.Base;
using OneGate.Shared.Models.Asset;

namespace OneGate.Backend.Rpc.Contracts.Asset.CreateAsset
{
    public class CreateAssetResponse: SuccessResponse
    {
        public AssetBaseDto Asset { get; set; }
    }
}