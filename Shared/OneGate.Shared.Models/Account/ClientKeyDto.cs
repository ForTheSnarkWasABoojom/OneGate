using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace OneGate.Shared.Models.Account
{
    public class ClientKeyDto
    {
        [MaxLength(100)]
        [FromQuery(Name = "client_key")]
        [JsonProperty("client_key")]
        public string ClientKey { get; set; }
    }
}