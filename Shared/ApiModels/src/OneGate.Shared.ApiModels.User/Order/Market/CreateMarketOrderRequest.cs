namespace OneGate.Shared.ApiModels.User.Order.Market
{
    public class CreateMarketOrderRequest : CreateOrderRequest
    {
        public override OrderType? Type => OrderType.MARKET;
    }
}