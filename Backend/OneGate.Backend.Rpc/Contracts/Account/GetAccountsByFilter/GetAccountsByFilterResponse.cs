using System.Collections.Generic;
using OneGate.Backend.Rpc.Contracts.Base;
using OneGate.Shared.Models.Account;

namespace OneGate.Backend.Rpc.Contracts.Account.GetAccountsByFilter
{
    public class GetAccountsByFilterResponse : SuccessResponse
    {
        public List<AccountDto> Accounts { get; set; }
    }
}