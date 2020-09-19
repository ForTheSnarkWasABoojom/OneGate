using OneGate.Shared.Models.Account;

namespace OneGate.Backend.Rpc.Contracts.Account.CreateAccount
{
    public class CreateAccountRequest
    {
        public CreateAccountDto Account { get; set; }
        public string ClientKey { get; set; }
    }
}