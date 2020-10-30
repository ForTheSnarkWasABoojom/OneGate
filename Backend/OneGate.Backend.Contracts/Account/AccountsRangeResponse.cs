using System.Collections.Generic;
using OneGate.Backend.Contracts.Common;
using OneGate.Shared.Models.Account;

namespace OneGate.Backend.Contracts.Account
{
    public class AccountsRangeResponse : SuccessResponse
    {
        public IEnumerable<AccountDto> Accounts { get; set; }
    }
}