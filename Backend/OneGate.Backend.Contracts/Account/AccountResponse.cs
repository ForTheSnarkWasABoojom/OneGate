using OneGate.Backend.Contracts.Common;
using OneGate.Shared.Models.Account;

namespace OneGate.Backend.Contracts.Account
{
    public class AccountResponse : SuccessResponse
    {
        public AccountDto Account { get; set; }
    }
}