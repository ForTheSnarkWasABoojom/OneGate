using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OneGate.Shared.ApiModels.Base;

namespace OneGate.Shared.ApiModels.User.Layout
{
    public class FilterLayoutsRequest : FilterRequest
    {
        [FromQuery(Name = "id")]
        public int? Id { get; set; }

        [MaxLength(50)]
        [FromQuery(Name = "name")]
        public string Name { get; set; }
    }
}