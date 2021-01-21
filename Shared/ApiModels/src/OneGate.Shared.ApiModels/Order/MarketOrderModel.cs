namespace OneGate.Shared.ApiModels.Order
{
    public class MarketOrderModel : OrderModel
    {
        public override OrderTypeModel? Type => OrderTypeModel.MARKET;
    }
}