using System.Collections.Generic;
using MassTransit.Topology;
using OneGate.Shared.ApiContracts.Account;

namespace OneGate.Backend.Transport.Contracts.Account
{
    [EntityName("response.accounts")]
    public class AccountsResponse
    {
        public IEnumerable<AccountDto> Accounts { get; set; }
    }
}