using MassTransit.Topology;

namespace OneGate.Backend.Transport.Contracts.Account
{
    [EntityName("request.account.delete")]
    public class DeleteAccount
    {
        public int Id { get; set; }
    }
}