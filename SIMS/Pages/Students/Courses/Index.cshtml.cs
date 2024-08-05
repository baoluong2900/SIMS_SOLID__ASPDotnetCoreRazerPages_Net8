using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIMS.Model;
using SIMS.Services;

namespace SIMS.Pages.Students.Courses
{
    public class IndexModel : PageModel
    {
        private readonly StudentCourseService _service;
        public INotyfService _notifyService { get; }
        public List<StudentCourse> StudentCourseList { get; set; }
        private string idLogin;

        public IndexModel(StudentCourseService service, INotyfService notifyService)
        {
            _service = service;
            _notifyService = notifyService;
        }

        public void OnGet()
        {
            idLogin = HttpContext.Session.GetString("idLogin");
            StudentCourseList = _service.GetStudentCoursesByStudentNo(idLogin).ToList();
        }
    }
}
