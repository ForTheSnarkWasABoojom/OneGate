using AutoMapper;
using OneGate.Backend.Core.Users.Contracts.Account;
using OneGate.Shared.ApiModels.Admin.Account;

namespace OneGate.Backend.Gateway.AdminApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountDto, AccountModel>();
        }
    }
}