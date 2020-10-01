using OneGate.Backend.Rpc.Contracts.Base;
using OneGate.Shared.Models.Common;
using OneGate.Shared.Models.Order;

namespace OneGate.Backend.Rpc.Contracts.Order.CreateOrder
{
    public class CreateOrderResponse : SuccessResponse
    {
        public CreatedResourceDto CreatedResource { get; set; }
    }
}