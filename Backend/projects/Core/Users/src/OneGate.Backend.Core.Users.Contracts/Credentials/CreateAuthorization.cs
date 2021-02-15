using MassTransit.Topology;
using Newtonsoft.Json;

namespace OneGate.Backend.Core.Users.Contracts.Credentials
{
    [EntityName("request.authorization.create")]
    public class CreateAuthorization
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        
        [JsonProperty("password")]
        public string Password { get; set; }
        
        [JsonProperty("is_admin")]
        public bool? IsAdmin { get; set; }
    }
}