using System;
using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Contracts
{
    public class BaseEvent
    {
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}