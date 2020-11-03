using System.Collections.Generic;
using OneGate.Backend.Contracts.Common;
using OneGate.Shared.Models.Asset;

namespace OneGate.Backend.Contracts.Asset
{
    public class AssetsRangeResponse : SuccessResponse
    {
        public IEnumerable<AssetBaseDto> Assets { get; set; }
    }
}