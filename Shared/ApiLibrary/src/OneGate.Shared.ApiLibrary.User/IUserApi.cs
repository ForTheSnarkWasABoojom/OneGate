using System.Threading.Tasks;
using OneGate.Shared.ApiModels.User.Account;

namespace OneGate.Shared.ApiLibrary.User
{
    public interface IUserApi
    {
        public Task<Account> GetAccount();
    }
}