using Microsoft.AspNetCore.Mvc;
using OneGate.Backend.Core.Shared.Api.Contracts;

namespace OneGate.Backend.Core.Users.Api.Contracts.Portfolio
{
    public class FilterPortfoliosDto : FilterDto
    {
        [FromQuery(Name = "id")]
        public int? Id { get; set; }

        [FromQuery(Name = "id")]
        public int? OwnerId { get; set; }
    }
}