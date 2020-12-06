namespace OneGate.Shared.Models.Order
{
    public class CreateMarketOrderDto : CreateOrderDto
    {
        public override OrderTypeDto? Type => OrderTypeDto.MARKET;
    }
}