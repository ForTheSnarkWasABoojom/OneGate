namespace OneGate.Shared.ApiModels.User.Order.Market
{
    public class MarketOrderModel : OrderModel
    {
        public override OrderTypeModel? Type => OrderTypeModel.MARKET;
    }
}