using OneGate.Backend.Rpc.Contracts.Base;
using OneGate.Shared.Models.Order;

namespace OneGate.Backend.Rpc.Contracts.Order.GetOrder
{
    public class GetOrderResponse : SuccessResponse
    {
        public OrderBaseDto Order { get; set; }
    }
}