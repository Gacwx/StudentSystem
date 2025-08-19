using AutoMapper;
using MyApp.Api.Models;
using MyApp.Api.Dtos;

namespace MyApp.Api.Mapping
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Student, StudentDto>();
            CreateMap<StudentCreateDto, Student>();
        }
    }
}
