using System;
using MassTransit.Topology;
using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Contracts
{
    [EntityName("event")]
    public class BaseEvent
    {
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}