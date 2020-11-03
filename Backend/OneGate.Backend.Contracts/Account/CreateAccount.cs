using MassTransit.Topology;
using OneGate.Shared.Models.Account;

namespace OneGate.Backend.Contracts.Account
{
    [EntityName("request.account.create")]
    public class CreateAccount
    {
        public CreateAccountDto Account { get; set; }
    }
}