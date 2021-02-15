using System.Threading.Tasks;
using OneGate.Backend.Core.Users.Contracts.Portfolio;
using OneGate.Backend.Transport.Bus.Contracts;
using OneGate.Backend.Transport.Contracts;

namespace OneGate.Backend.Core.Users.Services
{
    public interface IPortfolioService
    {
        public Task<CreatedResourceResponse> CreatePortfolioAsync(CreatePortfolio request); 
        public Task<PortfoliosResponse> GetPortfoliosAsync(GetPortfolios request); 
        public Task<SuccessResponse> DeletePortfolioAsync(DeletePortfolio request);
    }
}