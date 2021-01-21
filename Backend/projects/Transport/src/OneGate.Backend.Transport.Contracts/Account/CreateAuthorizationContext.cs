using MassTransit.Topology;
using OneGate.Backend.Transport.Dto.Account;

namespace OneGate.Backend.Transport.Contracts.Account
{
    [EntityName("request.authorization.create")]
    public class CreateAuthorizationContext
    {
        public AuthDto AuthDto;
    }
}