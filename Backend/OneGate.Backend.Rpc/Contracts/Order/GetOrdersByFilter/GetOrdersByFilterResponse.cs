using System.Collections.Generic;
using OneGate.Backend.Rpc.Contracts.Base;
using OneGate.Shared.Models.Order;

namespace OneGate.Backend.Rpc.Contracts.Order.GetOrdersByFilter
{
    public class GetOrdersByFilterResponse : SuccessResponse
    {
        public List<OrderBaseDto> Orders { get; set; }
    }
}