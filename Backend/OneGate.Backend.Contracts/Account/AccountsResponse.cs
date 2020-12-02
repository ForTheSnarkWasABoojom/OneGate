using System.Collections.Generic;
using MassTransit.Topology;
using OneGate.Shared.Models.Account;

namespace OneGate.Backend.Contracts.Account
{
    [EntityName("response.accounts")]
    public class AccountsResponse
    {
        public IEnumerable<AccountDto> Accounts { get; set; }
    }
}