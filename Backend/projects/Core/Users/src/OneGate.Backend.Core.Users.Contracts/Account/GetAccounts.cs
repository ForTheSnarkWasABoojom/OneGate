using MassTransit.Topology;
using Newtonsoft.Json;
using OneGate.Backend.Transport.Contracts;

namespace OneGate.Backend.Core.Users.Contracts.Account
{
    [EntityName("request.account.get")]
    public class GetAccounts : LargeDataRequest
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}