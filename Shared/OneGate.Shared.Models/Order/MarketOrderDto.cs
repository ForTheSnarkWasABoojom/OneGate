namespace OneGate.Shared.Models.Order
{
    public class MarketOrderDto : OrderDto
    {
        public override OrderTypeDto? Type => OrderTypeDto.MARKET;
    }
}