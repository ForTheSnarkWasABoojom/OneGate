using Microsoft.AspNetCore.Mvc;
using OneGate.Backend.Gateway.Shared.Api.Contracts;

namespace OneGate.Backend.Gateway.User.Api.Contracts.Order
{
    public class FilterOrdersRequest : FilterRequest
    {
        [FromQuery(Name = "id")]
        public int? Id { get; set; }
    }
}