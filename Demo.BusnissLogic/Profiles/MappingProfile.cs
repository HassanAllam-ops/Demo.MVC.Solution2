using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Demo.BusnissLogic.DataTransferObjects.Empolyees;
using Demo.DataAccess.Models.Empolyees;

namespace Demo.BusnissLogic.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Empolyee, EmpolyeeDto>()
                .ForMember(dest => dest.EmpGender, options => options.MapFrom(src => src.Gender))
                .ForMember(dest => dest.EmpType, options => options.MapFrom(src => src.EmpolyeeType))
                .ForMember(dest => dest.DepartmentName, options => options.MapFrom(src => src.Department == null ? "No Department" : src.Department.Name));

            CreateMap<Empolyee, EmpolyeeDetailsDto>()
               .ForMember(dest => dest.Gender, options => options.MapFrom(src => src.Gender))
               .ForMember(dest => dest.EmpolyeeType, options => options.MapFrom(src => src.EmpolyeeType))
               .ForMember(dest => dest.HiringDate, options => options.MapFrom(src => DateOnly.FromDateTime(src.HiringDate)))
                .ForMember(dest => dest.DepartmentName, options => options.MapFrom(src => src.Department == null ? "No Department" : src.Department.Name));

            CreateMap<CreatedEmpolyeeDto, Empolyee>()
                .ForMember(dest => dest.HiringDate, options => options.MapFrom(src => src.HiringDate.ToDateTime(new TimeOnly())));

            CreateMap<UpdatedEmpolyeeDto, Empolyee>()
                .ForMember(dest => dest.HiringDate, options => options.MapFrom(src => src.HiringDate.ToDateTime(new TimeOnly())));

            // May be if i want to reverse the mapping i can use the ReverseMap method 
        }
    }
}
