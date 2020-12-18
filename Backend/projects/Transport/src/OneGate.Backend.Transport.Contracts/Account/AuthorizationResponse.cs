using MassTransit.Topology;
using OneGate.Common.Models.Account;

namespace OneGate.Backend.Transport.Contracts.Account
{
    [EntityName("response.authorization")]
    public class AuthorizationResponse
    {
        public AccountDto Account { get; set; }
    }
}