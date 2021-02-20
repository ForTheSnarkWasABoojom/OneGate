using Microsoft.AspNetCore.Mvc;
using OneGate.Shared.ApiModels.Base;

namespace OneGate.Shared.ApiModels.User.Exchange
{
    public class FilterExchangesRequest : FilterRequest
    {
        [FromQuery(Name = "id")]
        public int? Id { get; set; }
        
        [FromQuery(Name = "title")]
        public string Title { get; set; }
    }
}