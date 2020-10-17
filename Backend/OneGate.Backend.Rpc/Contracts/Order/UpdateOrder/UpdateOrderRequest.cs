using OneGate.Shared.Models.Order;

namespace OneGate.Backend.Rpc.Contracts.Order.UpdateOrder
{
    public class UpdateOrderRequest
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public OrderStateDto StateDto { get; set; }
    }
}