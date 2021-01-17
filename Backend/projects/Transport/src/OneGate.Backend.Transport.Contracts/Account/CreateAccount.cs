using MassTransit.Topology;
using OneGate.Shared.ApiContracts.Account;

namespace OneGate.Backend.Transport.Contracts.Account
{
    [EntityName("request.account.create")]
    public class CreateAccount
    {
        public CreateAccountDto Account { get; set; }
    }
}