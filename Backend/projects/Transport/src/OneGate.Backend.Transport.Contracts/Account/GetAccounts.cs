using MassTransit.Topology;
using OneGate.Shared.ApiContracts.Account;

namespace OneGate.Backend.Transport.Contracts.Account
{
    [EntityName("request.account.get")]
    public class GetAccounts
    {
        public AccountFilterDto Filter { get; set; }
    }
}