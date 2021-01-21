using System.Collections.Generic;
using MassTransit.Topology;
using OneGate.Backend.Transport.Dto.Account;

namespace OneGate.Backend.Transport.Contracts.Account
{
    [EntityName("response.accounts")]
    public class AccountsResponse
    {
        public IEnumerable<AccountDto> Accounts { get; set; }
    }
}