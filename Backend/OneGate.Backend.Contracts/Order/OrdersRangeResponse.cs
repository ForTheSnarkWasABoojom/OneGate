using System.Collections.Generic;
using OneGate.Shared.Models.Order;

namespace OneGate.Backend.Contracts.Order
{
    public class OrdersRangeResponse
    {
        public IEnumerable<OrderBaseDto> Orders { get; set; }
    }
}