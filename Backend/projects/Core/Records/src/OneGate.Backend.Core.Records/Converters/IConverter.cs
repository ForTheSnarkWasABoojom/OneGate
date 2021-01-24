using OneGate.Backend.Core.Records.Database.Models;
using OneGate.Backend.Transport.Dto.Asset;
using OneGate.Backend.Transport.Dto.Exchange;
using OneGate.Backend.Transport.Dto.Layout;

namespace OneGate.Backend.Core.Records.Converters
{
    public interface IConverter
    {
        public Asset FromDto(CreateAssetDto src);
        public AssetDto ToDto(Asset src);
        
        public Exchange FromDto(CreateExchangeDto src);
        public ExchangeDto ToDto(Exchange src);
        
        public Layout FromDto(CreateLayoutDto src);
        public LayoutDto ToDto(Layout src);
    }
}