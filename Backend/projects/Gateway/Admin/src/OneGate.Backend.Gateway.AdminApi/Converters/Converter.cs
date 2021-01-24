using OneGate.Backend.Transport.Dto.Account;
using OneGate.Shared.ApiModels.Account;

namespace OneGate.Backend.Gateway.AdminApi.Converters
{
    public class Converter : IConverter
    {
        public AccountFilterDto ToDto(AccountFilterModel src)
        {
            return new AccountFilterDto
            {
                Count = src.Count,
                Email = src.Email,
                FirstName = src.FirstName,
                Id = src.Id,
                IsAdmin = src.IsAdmin,
                LastName = src.LastName,
                Shift = src.Shift
            };
        }

        public AccountModel FromDto(AccountDto src)
        {
            return new AccountModel
            {
                Email = src.Email,
                FirstName = src.FirstName,
                Id = src.Id,
                IsAdmin = src.IsAdmin,
                LastName = src.LastName
            };
        }
    }
}