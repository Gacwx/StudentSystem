using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyApp.Api.Models;
using MyApp.Api.Dtos;

namespace MyApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IMapper _mapper;

        public DepartmentsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DepartmentDto>> Get()
        {
            var departments = new List<Department>
            {
                new Department { Id = 1, Name = "Computer Science" },
                new Department { Id = 2, Name = "Mathematics" },
                new Department { Id = 3, Name = "Physics" },
                new Department { Id = 4, Name = "Biology" },
                new Department { Id = 5, Name = "Engineering" }
            };

            var result = _mapper.Map<List<DepartmentDto>>(departments);
            return Ok(result);
        }
    }
}
