using MassTransit.Topology;
using Newtonsoft.Json;

namespace OneGate.Backend.Core.Users.Contracts.Account
{
    [EntityName("request.account.create")]
    public class CreateAccount
    {
        [JsonProperty("account")]
        public AccountDto Account { get; set; }
        
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}