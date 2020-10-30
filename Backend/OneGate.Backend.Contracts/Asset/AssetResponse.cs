using OneGate.Backend.Contracts.Common;
using OneGate.Shared.Models.Asset;

namespace OneGate.Backend.Contracts.Asset
{
    public class AssetResponse : SuccessResponse
    {
        public AssetBaseDto Asset { get; set; }
    }
}