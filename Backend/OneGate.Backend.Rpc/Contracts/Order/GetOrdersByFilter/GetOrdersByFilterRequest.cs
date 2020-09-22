using OneGate.Shared.Models.Order;

namespace OneGate.Backend.Rpc.Contracts.Order.GetOrdersByFilter
{
    public class GetOrdersByFilterRequest
    {
        public OrderBaseFilterDto Filter { get; set; }
        public int AccountId { get; set; }
    }
}