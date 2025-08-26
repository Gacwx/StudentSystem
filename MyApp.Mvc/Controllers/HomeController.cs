using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using MyApp.Mvc.Models;

namespace MyApp.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _factory;
        public HomeController(IHttpClientFactory factory) => _factory = factory;

        public async Task<IActionResult> Index(int? departmentId)
        {
            var api = _factory.CreateClient("Api");

            var departments = await api.GetFromJsonAsync<List<DepartmentVm>>("api/departments");
            ViewBag.Departments = departments ?? new();

            string url = "api/students";
            if (departmentId.HasValue)
                url += $"?departmentId={departmentId}";

            var students = await api.GetFromJsonAsync<List<StudentVm>>(url);

            return View(students ?? new List<StudentVm>());
        }

        public async Task<IActionResult> GetStudentsByDepartment(int? departmentId)
        {
            var api = _factory.CreateClient("Api");

            string url = "api/students";
            if (departmentId.HasValue)
                url += $"?departmentId={departmentId}";

            var students = await api.GetFromJsonAsync<List<StudentVm>>(url);

            var result = students.Select(s => new
            {
                name = s.Name,
                email = s.Email,
                gender = s.Gender,
                departmentName = s.Department?.Name
            });

            return Json(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] StudentVm student)
        {
            var api = _factory.CreateClient("Api");
            var response = await api.PostAsJsonAsync("api/students", student);
            var createdStudent = await response.Content.ReadFromJsonAsync<StudentVm>();

            return Json(new
            {
                name = createdStudent.Name,
                email = createdStudent.Email,
                gender = createdStudent.Gender,
                departmentName = createdStudent.Department?.Name,
                departmentId = createdStudent.DepartmentId
            });
        }
    }
}
