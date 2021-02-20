namespace OneGate.Backend.Core.Users.Api.Contracts.Order.Market
{
    public class CreateMarketOrderDto : OrderDto
    {
        public override OrderTypeDto? Type => OrderTypeDto.MARKET;
    }
}