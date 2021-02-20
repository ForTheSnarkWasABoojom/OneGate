using Microsoft.AspNetCore.Mvc;
using OneGate.Backend.Core.Shared.Api.Contracts;

namespace OneGate.Backend.Core.Assets.Api.Contracts.Exchange
{
    public class FilterExchangesDto : FilterDto
    {
        [FromQuery(Name = "id")]
        public int? Id { get; set; }
        
        [FromQuery(Name = "title")]
        public string Title { get; set; }
    }
}