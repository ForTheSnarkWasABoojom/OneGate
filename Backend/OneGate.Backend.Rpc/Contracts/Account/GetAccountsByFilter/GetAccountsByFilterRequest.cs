using OneGate.Shared.Models.Account;

namespace OneGate.Backend.Rpc.Contracts.Account.GetAccountsByFilter
{
    public class GetAccountsByFilterRequest
    {
        public AccountFilterDto Filter { get; set; }
    }
}