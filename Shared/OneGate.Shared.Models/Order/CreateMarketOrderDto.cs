namespace OneGate.Shared.Models.Order
{
    public class CreateMarketOrderDto : CreateOrderBaseDto
    {
        public override OrderTypeDto Type { get; } = OrderTypeDto.MARKET;
    }
}