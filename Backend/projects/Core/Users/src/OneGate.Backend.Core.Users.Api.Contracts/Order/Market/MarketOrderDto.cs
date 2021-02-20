namespace OneGate.Backend.Core.Users.Api.Contracts.Order.Market
{
    public class MarketOrderDto : OrderDto
    {
        public override OrderTypeDto? Type => OrderTypeDto.MARKET;
    }
}