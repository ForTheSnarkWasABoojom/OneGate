using Microsoft.AspNetCore.Mvc;
using OneGate.Shared.ApiModels.Base;

namespace OneGate.Shared.ApiModels.User.Asset
{
    public class AssetFilterModel : FilterModel
    {
        [FromQuery(Name = "id")]
        public int? Id { get; set; }
    }
}