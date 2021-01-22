using System.Threading.Tasks;
using OneGate.Shared.ApiModels.Account;

namespace OneGate.Shared.ApiLibrary.User
{
    public interface IUserApi
    {
        public Task<AccountModel> GetMyAccount();
    }
}