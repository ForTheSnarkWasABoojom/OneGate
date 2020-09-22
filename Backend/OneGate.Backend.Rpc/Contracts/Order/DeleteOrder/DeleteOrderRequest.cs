namespace OneGate.Backend.Rpc.Contracts.Order.DeleteOrder
{
    public class DeleteOrderRequest
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
    }
}