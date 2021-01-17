using MassTransit.Topology;
using OneGate.Shared.ApiContracts.Account;

namespace OneGate.Backend.Transport.Contracts.Account
{
    [EntityName("response.authorization")]
    public class AuthorizationResponse
    {
        public AccountDto Account { get; set; }
    }
}