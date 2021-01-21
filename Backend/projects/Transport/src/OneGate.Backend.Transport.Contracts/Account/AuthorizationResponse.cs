using MassTransit.Topology;
using OneGate.Backend.Transport.Dto.Account;

namespace OneGate.Backend.Transport.Contracts.Account
{
    [EntityName("response.authorization")]
    public class AuthorizationResponse
    {
        public AccountDto Account { get; set; }
    }
}