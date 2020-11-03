using MassTransit.Topology;
using OneGate.Shared.Models.Account;

namespace OneGate.Backend.Contracts.Account
{
    [EntityName("request.account.get")]
    public class GetAccounts
    {
        public AccountFilterDto Filter { get; set; }
    }
}