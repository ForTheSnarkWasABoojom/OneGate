namespace OneGate.Shared.Models.Order
{
    public class MarketOrderDto : OrderBaseDto
    {
        public override OrderTypeDto Type { get; } = OrderTypeDto.MARKET;
    }
}