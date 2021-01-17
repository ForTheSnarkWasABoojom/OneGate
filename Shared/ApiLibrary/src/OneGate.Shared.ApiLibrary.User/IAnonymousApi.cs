using System.Threading.Tasks;

namespace OneGate.Shared.ApiLibrary.User
{
    public interface IAnonymousApi
    {
        public Task<string> GetAccessTokenAsync(string username, string password);
    }
}