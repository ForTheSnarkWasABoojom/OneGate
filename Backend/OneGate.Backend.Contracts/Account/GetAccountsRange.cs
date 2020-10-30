using MassTransit.Topology;
using OneGate.Shared.Models.Account;

namespace OneGate.Backend.Contracts.Account
{
    [EntityName("account.get_range")]
    public class GetAccountsRange
    {
        public AccountFilterDto Filter { get; set; }
    }
}