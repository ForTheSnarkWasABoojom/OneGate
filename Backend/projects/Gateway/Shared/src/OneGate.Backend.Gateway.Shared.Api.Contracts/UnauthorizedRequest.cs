using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Backend.Gateway.Shared.Api.Contracts
{
    public class UnauthorizedRequest
    {
        [Required]
        [MaxLength(100)]
        [JsonProperty("client_fingerprint")]
        public string ClientFingerprint { get; set; }
    }
}