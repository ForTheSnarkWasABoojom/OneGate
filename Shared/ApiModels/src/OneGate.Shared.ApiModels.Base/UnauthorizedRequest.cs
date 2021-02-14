using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.Base
{
    public class UnauthorizedRequest
    {
        [Required]
        [MaxLength(100)]
        [JsonProperty("client_fingerprint")]
        public string ClientFingerprint { get; set; }
    }
}