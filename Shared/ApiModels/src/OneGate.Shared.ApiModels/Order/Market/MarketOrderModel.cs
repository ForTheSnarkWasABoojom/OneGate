namespace OneGate.Shared.ApiModels.Order.Market
{
    public class MarketOrderModel : OrderModel
    {
        public override OrderTypeModel? Type => OrderTypeModel.MARKET;
    }
}