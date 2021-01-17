namespace OneGate.Shared.ApiContracts.Order
{
    public class CreateMarketOrderDto : CreateOrderDto
    {
        public override OrderTypeDto? Type => OrderTypeDto.MARKET;
    }
}