using AutoMapper;
using Cleverbit.CodingCase.Application.Models;
using Cleverbit.CodingCase.Application.Requests.Commands;
using Cleverbit.CodingCase.Domain.Models;

namespace Cleverbit.CodingCase.Application.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddEmployeeCommand, Employee>();
        CreateMap<AddRegionCommand, Region>();

        CreateMap<Employee, EmployeeViewModel>()
            .ForMember(x => x.RegionName, opt => opt.MapFrom(src => src.Region.Name));

        CreateMap<Employee, RegionEmployeeViewModel>()
         .ForMember(x => x.RegionName, opt => opt.MapFrom(src => src.Region.Name))
         .ForMember(x => x.RegionId, opt => opt.MapFrom(src => src.RegionId));
    }
}