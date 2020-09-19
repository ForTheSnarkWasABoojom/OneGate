using OneGate.Backend.Rpc.Contracts.Base;
using OneGate.Shared.Models.Account;

namespace OneGate.Backend.Rpc.Contracts.Account.CreateToken
{
    public class CreateTokenResponse : SuccessResponse
    {
        public AccountDto Account { get; set; }
        public bool IsAdmin { get; set; }
    }
}