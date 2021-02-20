using AutoMapper;
using OneGate.Backend.Core.Users.Api.Contracts.Account;
using OneGate.Shared.ApiModels.Admin.Account;

namespace OneGate.Backend.Gateway.Admin.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMapForAccount();
        }
        
        private void CreateMapForAccount()
        {
            CreateMap<AccountDto, Account>();
            
            CreateMap<FilterAccountsRequest, FilterAccountsDto>();
        }
    }
}