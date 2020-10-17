using OneGate.Backend.Rpc.Contracts.Base;
using OneGate.Shared.Models.Account;
using OneGate.Shared.Models.Common;

namespace OneGate.Backend.Rpc.Contracts.Account.CreateAccount
{
    public class CreateAccountResponse : SuccessResponse
    {
        public ResourceDto Resource { get; set; }
    }
}