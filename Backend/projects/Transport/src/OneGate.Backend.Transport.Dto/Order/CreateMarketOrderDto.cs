namespace OneGate.Backend.Transport.Dto.Order
{
    public class CreateMarketOrderDto : CreateOrderDto
    {
        public override OrderTypeDto? Type => OrderTypeDto.MARKET;
    }
}