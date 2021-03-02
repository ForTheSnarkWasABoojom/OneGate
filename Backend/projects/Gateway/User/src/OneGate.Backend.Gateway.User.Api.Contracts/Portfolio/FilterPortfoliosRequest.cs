using Microsoft.AspNetCore.Mvc;
using OneGate.Backend.Gateway.Shared.Api.Contracts;

namespace OneGate.Backend.Gateway.User.Api.Contracts.Portfolio
{
    public class FilterPortfoliosRequest : FilterRequest
    {
        [FromQuery(Name = "id")]
        public int? Id { get; set; }
    }
}