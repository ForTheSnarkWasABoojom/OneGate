using OneGate.Backend.Rpc.Contracts.Base;
using OneGate.Shared.Models.Order;

namespace OneGate.Backend.Rpc.Contracts.Order.UpdateOrder
{
    public class UpdateOrderResponse:SuccessResponse
    {
        public OrderBaseDto Order { get; set; }
    }
}