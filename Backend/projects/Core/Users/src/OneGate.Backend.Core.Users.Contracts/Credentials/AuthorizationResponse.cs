using MassTransit.Topology;
using Newtonsoft.Json;
using OneGate.Backend.Core.Users.Contracts.Account;

namespace OneGate.Backend.Core.Users.Contracts.Credentials
{
    [EntityName("response.authorization")]
    public class AuthorizationResponse
    {
        [JsonProperty("authorized_account")]
        public AccountDto AuthorizedAccount { get; set; }
    }
}