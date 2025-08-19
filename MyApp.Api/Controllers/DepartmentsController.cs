using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MyApp.Api.Data;
using MyApp.Api.Dtos;

namespace MyApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json", "application/xml")] 
    public class DepartmentsController : ControllerBase
    {
        private readonly ModelContext _db;
        private readonly IMapper _mapper;
        public DepartmentsController(ModelContext db, IMapper mapper) { _db = db; _mapper = mapper; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetAll()
        {
            var list = await _db.Departments.AsNoTracking().ToListAsync();
            return Ok(_mapper.Map<IEnumerable<DepartmentDto>>(list));
        }
    }
}