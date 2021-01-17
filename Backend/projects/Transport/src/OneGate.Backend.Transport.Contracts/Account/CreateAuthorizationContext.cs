using MassTransit.Topology;
using OneGate.Shared.ApiContracts.Account;

namespace OneGate.Backend.Transport.Contracts.Account
{
    [EntityName("request.authorization.create")]
    public class CreateAuthorizationContext
    {
        public OAuthDto AuthDto;
    }
}