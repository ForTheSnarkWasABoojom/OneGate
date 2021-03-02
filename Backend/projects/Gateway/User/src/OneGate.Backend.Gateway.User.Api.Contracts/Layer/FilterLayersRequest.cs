using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OneGate.Backend.Gateway.Shared.Api.Contracts;

namespace OneGate.Backend.Gateway.User.Api.Contracts.Layer
{
    public class FilterLayersRequest : FilterRequest
    {
        [FromQuery(Name = "id")]
        public int? Id { get; set; }

        [MaxLength(50)]
        [FromQuery(Name = "name")]
        public string Name { get; set; }
    }
}