namespace OneGate.Backend.Gateway.User.Api.Contracts.Order.Market
{
    public class CreateMarketOrderRequest : CreateOrderRequest
    {
        public override OrderType? Type => OrderType.MARKET;
    }
}