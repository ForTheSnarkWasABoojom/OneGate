using Microsoft.AspNetCore.Mvc;
using OneGate.Backend.Core.Shared.Api.Contracts;

namespace OneGate.Backend.Core.Users.Api.Contracts.Order
{
    public class FilterOrdersDto : FilterDto
    {
        [FromQuery(Name = "id")]
        public int? Id { get; set; }
        
        [FromQuery(Name = "owner_id")]
        public int? OwnerId { get; set; }
    }
}