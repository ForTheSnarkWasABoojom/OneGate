namespace OneGate.Backend.Gateway.User.Api.Contracts.Order.Market
{
    public class MarketOrderModel : OrderModel
    {
        public override OrderType? Type => OrderType.MARKET;
    }
}