using AutoMapper;
using DTO.Employment;
using Models.Employment;

namespace WebClient.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(a => a.FullName, a => a.MapFrom(b => $"{b.FirstName} {b.LastName}"));
        }
    }
}
