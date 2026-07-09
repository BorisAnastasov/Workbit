using AutoMapper;
using Workbit.Application.Features.Auth.Shared;
using Workbit.WebApi.Contracts.Auth;

namespace Workbit.WebApi.Mapping
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<AuthResult, AuthResponseDto>();
        }
    }
}
