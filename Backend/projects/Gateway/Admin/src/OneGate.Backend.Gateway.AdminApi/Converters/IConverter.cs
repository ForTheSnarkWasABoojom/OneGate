using OneGate.Backend.Transport.Dto.Account;
using OneGate.Shared.ApiModels.Account;

namespace OneGate.Backend.Gateway.AdminApi.Converters
{
    public interface IConverter
    {
        public AccountFilterDto ToDto(AccountFilterModel src);
        public AccountModel FromDto(AccountDto src);
    }
}