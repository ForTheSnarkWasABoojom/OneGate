using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OneGate.Common.Models.Common;

namespace OneGate.Common.Models.Layout
{
    public class LayoutFilterDto : FilterDto
    {
        [FromQuery(Name = "id")]
        [JsonProperty("id")]
        public int? Id { get; set; }

        [MaxLength(50)]
        [FromQuery(Name = "name")]
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}