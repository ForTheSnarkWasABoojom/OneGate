namespace OneGate.Shared.ApiModels.User.Order.Market
{
    public class CreateMarketOrderModel : CreateOrderModel
    {
        public override OrderTypeModel? Type => OrderTypeModel.MARKET;
    }
}