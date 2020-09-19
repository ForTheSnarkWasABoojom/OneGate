using OneGate.Shared.Models.Asset;

namespace OneGate.Backend.Rpc.Contracts.Asset.GetAssetsByFilter
{
    public class GetAssetsByFilterRequest
    {
        public AssetBaseFilterDto Filter { get; set; }
    }
}