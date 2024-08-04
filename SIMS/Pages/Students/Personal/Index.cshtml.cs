using Microsoft.AspNetCore.Mvc.RazorPages;
using SIMS.Model;
using SIMS.Services;

namespace SIMS.Pages.Students.Personal
{
    public class IndexModel : PageModel
    {
        private readonly StudentService _service;
        public IEnumerable<Student> StudentList { get; set; }
        public Student newStudent { get; set; }
        public IndexModel(StudentService service)
        {
            _service = service;
        }
        public void OnGet()
        {
            StudentList = _service.GetStudents();
        }
    }
}
