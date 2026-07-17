using AutoMapper;
using Workbit.Application.Features.Employee.GetEmployeeById;
using Workbit.Domain.Entities;
using Workbit.Domain.Entities.Account;

namespace Workbit.Application.Common.Mappings
{
    public class GetEmployeeByIdProfile:Profile
    {
        public GetEmployeeByIdProfile()
        {
            CreateMap<Employee, GetEmployeeByIdResult>()
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.ApplicationUser.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.ApplicationUser.LastName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.ApplicationUser.PhoneNumber))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.ApplicationUser.DateOfBirth))
                .ForMember(dest=>dest.UserId, opt=> opt.MapFrom(src=>src.ApplicationUser.Id))
                .ForMember(dest => dest.JobName, opt => opt.MapFrom(src => src.Job.Title));
        }
    }
}
