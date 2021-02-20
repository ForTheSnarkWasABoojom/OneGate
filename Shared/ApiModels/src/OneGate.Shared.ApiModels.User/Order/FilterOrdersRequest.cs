using Microsoft.AspNetCore.Mvc;
using OneGate.Shared.ApiModels.Base;

namespace OneGate.Shared.ApiModels.User.Order
{
    public class FilterOrdersRequest : FilterRequest
    {
        [FromQuery(Name = "id")]
        public int? Id { get; set; }
    }
}