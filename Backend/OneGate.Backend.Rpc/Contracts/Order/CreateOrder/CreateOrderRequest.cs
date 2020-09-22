using OneGate.Shared.Models.Order;

namespace OneGate.Backend.Rpc.Contracts.Order.CreateOrder
{
    public class CreateOrderRequest
    {
        public CreateOrderBaseDto Order { get; set; }
        public int AccountId { get; set; }
    }
}