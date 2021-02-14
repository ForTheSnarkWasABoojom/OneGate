using MassTransit.Topology;
using Newtonsoft.Json;

namespace OneGate.Backend.Core.Users.Contracts.Account
{
    [EntityName("request.account.delete")]
    public class DeleteAccount
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}