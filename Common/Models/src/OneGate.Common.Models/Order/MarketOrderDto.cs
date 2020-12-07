namespace OneGate.Common.Models.Order
{
    public class MarketOrderDto : OrderDto
    {
        public override OrderTypeDto? Type => OrderTypeDto.MARKET;
    }
}