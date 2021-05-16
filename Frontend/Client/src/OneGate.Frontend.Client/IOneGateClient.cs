using System.Collections.ObjectModel;
using System.Threading.Tasks;
using OneGate.Backend.Gateway.User.Api.Contracts.Account;
using OneGate.Backend.Gateway.User.Api.Contracts.Credentials;
using OneGate.Backend.Gateway.User.Api.Contracts.Timeseries;

namespace OneGate.Frontend.Client
{
    public interface IOneGateClient
    {
        public Task CreateAccountAsync(CreateAccountRequest model);

        public Task<TokenResponse> SignInAsync(AuthRequest model);

        public Task<ObservableCollection<OhlcModel>> GetOhlcData();

        public Task<ObservableCollection<GraphLayer>> GetGraphLayers();
    }
}