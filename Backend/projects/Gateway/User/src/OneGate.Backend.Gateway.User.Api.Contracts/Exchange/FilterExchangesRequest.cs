using Microsoft.AspNetCore.Mvc;
using OneGate.Backend.Gateway.Shared.Api.Contracts;

namespace OneGate.Backend.Gateway.User.Api.Contracts.Exchange
{
    public class FilterExchangesRequest : FilterRequest
    {
        [FromQuery(Name = "id")]
        public int? Id { get; set; }
        
        [FromQuery(Name = "title")]
        public string Title { get; set; }
    }
}