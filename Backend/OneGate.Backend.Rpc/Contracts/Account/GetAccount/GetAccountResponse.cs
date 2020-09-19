using OneGate.Backend.Rpc.Contracts.Base;
using OneGate.Shared.Models.Account;

namespace OneGate.Backend.Rpc.Contracts.Account.GetAccount
{
    public class GetAccountResponse : SuccessResponse
    {
        public AccountDto Account { get; set; }
    }
}