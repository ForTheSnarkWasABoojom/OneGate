using MassTransit.Topology;
using OneGate.Common.Models.Account;

namespace OneGate.Backend.Transport.Contracts.Account
{
    [EntityName("request.authorization.create")]
    public class CreateAuthorizationContext
    {
        public OAuthDto AuthDto;
    }
}