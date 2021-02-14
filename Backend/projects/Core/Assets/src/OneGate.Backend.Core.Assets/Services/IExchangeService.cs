using System.Threading.Tasks;
using OneGate.Backend.Core.Assets.Contracts.Exchange;

namespace OneGate.Backend.Core.Assets.Services
{
    public interface IExchangeService
    {
        public Task<ExchangesResponse> GetExchangesAsync(GetExchanges request);
    }
}