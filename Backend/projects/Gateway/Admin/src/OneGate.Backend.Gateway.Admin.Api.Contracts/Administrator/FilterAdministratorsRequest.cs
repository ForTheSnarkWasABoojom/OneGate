using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OneGate.Backend.Gateway.Shared.Api.Contracts;

namespace OneGate.Backend.Gateway.Admin.Api.Contracts.Administrator
{
    public class FilterAdministratorsRequest : FilterRequest
    {
        [FromQuery(Name = "id")]
        public int? Id { get; set; }

        [MaxLength(30)]
        [FromQuery(Name = "email")]
        public string Email { get; set; }
    }
}