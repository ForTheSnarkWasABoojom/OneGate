namespace OneGate.Shared.ApiModels.Order.Market
{
    public class CreateMarketOrderModel : CreateOrderModel
    {
        public override OrderTypeModel? Type => OrderTypeModel.MARKET;
    }
}