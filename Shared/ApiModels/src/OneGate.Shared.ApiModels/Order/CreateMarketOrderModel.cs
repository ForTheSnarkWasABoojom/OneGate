namespace OneGate.Shared.ApiModels.Order
{
    public class CreateMarketOrderModel : CreateOrderModel
    {
        public override OrderTypeModel? Type => OrderTypeModel.MARKET;
    }
}