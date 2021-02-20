namespace OneGate.Shared.ApiModels.User.Order.Market
{
    public class MarketOrder : Order
    {
        public override OrderType? Type => OrderType.MARKET;
    }
}