using MassTransit.Topology;

namespace OneGate.Backend.Contracts.Account
{
    [EntityName("account.get")]
    public class GetAccount
    {
        public int? Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}