using static MyApp.Mvc.Controllers.HomeController;

namespace MyApp.Mvc.Models
{
    public class StudentVm
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Gender { get; set; } = "";
        public int DepartmentId { get; set; }        // Əlavə etdik
        public DepartmentVm? Department { get; set; }
    }
}
