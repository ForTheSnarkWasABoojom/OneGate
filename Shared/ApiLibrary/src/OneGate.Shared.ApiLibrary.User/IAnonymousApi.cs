using System.Threading.Tasks;
using OneGate.Shared.ApiModels.Account;

namespace OneGate.Shared.ApiLibrary.User
{
    public interface IAnonymousApi
    {
        public Task<string> GetTokenAsync(string username, string password);
        public Task<int> CreateAccountAsync(CreateAccountModel model);
    }
}