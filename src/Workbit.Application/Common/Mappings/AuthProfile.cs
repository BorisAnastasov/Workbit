using AutoMapper;
using Workbit.Application.Features.Auth.Shared;
using Workbit.Domain.Entities.Account;

namespace Workbit.Application.Common.Mappings
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<ApplicationUser, AuthResult>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Roles, opt => opt.Ignore())
                .ForMember(dest => dest.Token, opt => opt.Ignore())
                .ForMember(dest => dest.Expires, opt => opt.Ignore());
        }
    }
}
