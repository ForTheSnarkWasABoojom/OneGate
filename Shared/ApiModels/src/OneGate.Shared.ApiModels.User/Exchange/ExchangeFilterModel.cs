using Microsoft.AspNetCore.Mvc;
using OneGate.Shared.ApiModels.Base;

namespace OneGate.Shared.ApiModels.User.Exchange
{
    public class ExchangeFilterModel : FilterModel
    {
        [FromQuery(Name = "id")]
        public int? Id { get; set; }
    }
}