using OneGate.Backend.Rpc.Contracts.Base;
using OneGate.Shared.Models.Asset;
using OneGate.Shared.Models.Common;

namespace OneGate.Backend.Rpc.Contracts.Asset.CreateAsset
{
    public class CreateAssetResponse: SuccessResponse
    {
        public ResourceDto Resource { get; set; }
    }
}