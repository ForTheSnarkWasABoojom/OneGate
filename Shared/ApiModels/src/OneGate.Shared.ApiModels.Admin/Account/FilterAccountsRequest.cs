using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OneGate.Shared.ApiModels.Base;

namespace OneGate.Shared.ApiModels.Admin.Account
{
    public class FilterAccountsRequest : FilterRequest
    {
        [FromQuery(Name = "id")]
        public int? Id { get; set; }

        [MaxLength(30)]
        [FromQuery(Name = "email")]
        public string Email { get; set; }
    }
}