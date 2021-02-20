using Microsoft.AspNetCore.Mvc;
using OneGate.Shared.ApiModels.Base;

namespace OneGate.Shared.ApiModels.User.Portfolio
{
    public class FilterPortfoliosRequest : FilterRequest
    {
        [FromQuery(Name = "id")]
        public int? Id { get; set; }
    }
}