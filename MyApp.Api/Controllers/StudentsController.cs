using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MyApp.Api.Data;
using MyApp.Api.Dtos;
using MyApp.Api.Models;

namespace MyApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json", "application/xml")] 
    public class StudentsController : ControllerBase
    {
        private readonly ModelContext _context;
        private readonly IMapper _mapper;
        public StudentsController(ModelContext context, IMapper mapper) 
        { 
            _context = context; _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAll([FromQuery] int? departmentId)
        {
            var q = _context.Students.Include(s => s.Department).AsQueryable();
            if (departmentId.HasValue) q = q.Where(s => s.DepartmentId == departmentId.Value);
            var list = await q.AsNoTracking().ToListAsync();
            return Ok(_mapper.Map<IEnumerable<StudentDto>>(list));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> Get(int id)
        {
            var s = await _context.Students.Include(x => x.Department).FirstOrDefaultAsync(x => x.Id == id);
            if (s == null) return NotFound();
            return Ok(_mapper.Map<StudentDto>(s));
        }

        [HttpPost]
        [Consumes("application/json", "application/xml")] 
        public async Task<ActionResult<StudentDto>> Create([FromBody] StudentCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var entity = _mapper.Map<Student>(dto);
            _context.Students.Add(entity);
            await _context.SaveChangesAsync();
            var created = await _context.Students.Include(d => d.Department).FirstAsync(s => s.Id == entity.Id);
            var result = _mapper.Map<StudentDto>(created);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StudentCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var existing = await _context.Students.FindAsync(id);
            if (existing == null) return NotFound();
            existing.Name = dto.Name; existing.Email = dto.Email; existing.Gender = dto.Gender; existing.DepartmentId = dto.DepartmentId;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var e = await _context.Students.FindAsync(id);
            if (e == null) return NotFound();
            _context.Students.Remove(e);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
