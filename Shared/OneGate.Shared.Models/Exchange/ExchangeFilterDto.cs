using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using OneGate.Shared.Models.Common;

namespace OneGate.Shared.Models.Exchange
{
    public class ExchangeFilterDto : FilterBaseDto
    {
        [MaxLength(50)]
        [FromQuery(Name = "title")]
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}