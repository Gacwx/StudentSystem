using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace MyApp.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _factory;
        public HomeController(IHttpClientFactory factory) => _factory = factory;

        public async Task<IActionResult> Index()
        {
            var api = _factory.CreateClient("Api");
            var departments = await api.GetFromJsonAsync<List<DepartmentVm>>("api/departments");
            ViewBag.Departments = departments ?? new();
            return View();
        }
    }

    public class DepartmentVm { public int Id { get; set; } public string Name { get; set; } }
}
