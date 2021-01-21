using MassTransit.Topology;
using OneGate.Backend.Transport.Dto.Account;

namespace OneGate.Backend.Transport.Contracts.Account
{
    [EntityName("request.account.get")]
    public class GetAccounts
    {
        public AccountFilterDto Filter { get; set; }
    }
}