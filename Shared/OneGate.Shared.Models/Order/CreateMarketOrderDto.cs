namespace OneGate.Shared.Models.Order
{
    public class CreateMarketOrderDto : CreateOrderBaseDto
    {
        public override OrderTypeDto? Type => OrderTypeDto.MARKET;
    }
}