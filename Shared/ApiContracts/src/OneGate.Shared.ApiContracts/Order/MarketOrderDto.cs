namespace OneGate.Shared.ApiContracts.Order
{
    public class MarketOrderDto : OrderDto
    {
        public override OrderTypeDto? Type => OrderTypeDto.MARKET;
    }
}