using AutoMapper;
using YourNamespace.Models;
using YourNamespace.DTOs;
using MyApp.Data.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Student, StudentDto>().ReverseMap();
        CreateMap<Department, DepartmentDto>().ReverseMap();
    }
}

