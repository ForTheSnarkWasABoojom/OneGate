using System.Collections.Generic;
using MassTransit.Topology;
using Newtonsoft.Json;

namespace OneGate.Backend.Core.Users.Contracts.Account
{
    [EntityName("response.accounts")]
    public class AccountsResponse
    {
        [JsonProperty("accounts")]
        public IEnumerable<AccountDto> Accounts { get; set; }
    }
}