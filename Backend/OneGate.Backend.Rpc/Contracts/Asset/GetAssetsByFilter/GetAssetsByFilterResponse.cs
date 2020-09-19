using System.Collections.Generic;
using OneGate.Backend.Rpc.Contracts.Base;
using OneGate.Shared.Models.Asset;

namespace OneGate.Backend.Rpc.Contracts.Asset.GetAssetsByFilter
{
    public class GetAssetsByFilterResponse : SuccessResponse
    {
        public List<AssetBaseDto> Assets { get; set; }
    }
}