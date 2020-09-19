using OneGate.Backend.Rpc.Contracts.Base;
using OneGate.Shared.Models.Account;

namespace OneGate.Backend.Rpc.Contracts.Account.CreateAccount
{
    public class CreateAccountResponse : SuccessResponse
    {
        public AccountDto Account { get; set; }
    }
}