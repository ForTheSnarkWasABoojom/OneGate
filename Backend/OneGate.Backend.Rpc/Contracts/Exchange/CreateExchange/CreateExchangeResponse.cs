using OneGate.Backend.Rpc.Contracts.Base;
using OneGate.Shared.Models.Common;
using OneGate.Shared.Models.Exchange;

namespace OneGate.Backend.Rpc.Contracts.Exchange.CreateExchange
{
    public class CreateExchangeResponse : SuccessResponse
    {
        public ResourceDto Resource { get; set; }
    }
}