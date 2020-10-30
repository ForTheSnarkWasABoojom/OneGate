using MassTransit.Topology;

namespace OneGate.Backend.Contracts.Account
{
    [EntityName("account.delete")]
    public class DeleteAccount
    {
        public int Id { get; set; }
    }
}