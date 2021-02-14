namespace OneGate.Backend.Core.Users.Contracts.Order
{
    public class MarketOrderDto: OrderDto
    {
        public override OrderTypeDto? Type => OrderTypeDto.MARKET;
    }
}