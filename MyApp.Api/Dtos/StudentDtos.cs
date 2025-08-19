using System.ComponentModel.DataAnnotations;

namespace MyApp.Api.Dtos
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DepartmentDto Department { get; set; }
    }

    public class StudentCreateDto
    {
        [Required, MaxLength(100)] public string Name { get; set; }
        [Required, EmailAddress] public string Email { get; set; }
        [Required] public string Gender { get; set; }
        [Required] public int DepartmentId { get; set; }
    }
}