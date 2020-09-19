using OneGate.Backend.Rpc.Contracts.Base;
using OneGate.Shared.Models.Asset;

namespace OneGate.Backend.Rpc.Contracts.Asset.GetAsset
{
    public class GetAssetResponse:SuccessResponse
    {
        public AssetBaseDto Asset { get; set; }
    }
}