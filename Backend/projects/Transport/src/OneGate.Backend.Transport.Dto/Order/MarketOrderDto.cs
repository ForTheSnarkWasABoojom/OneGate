namespace OneGate.Backend.Transport.Dto.Order
{
    public class MarketOrderDto : OrderDto
    {
        public override OrderTypeDto? Type => OrderTypeDto.MARKET;
    }
}