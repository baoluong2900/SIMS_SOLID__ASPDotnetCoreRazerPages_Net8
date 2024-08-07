using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIMS.Model;
using SIMS.Services;

namespace SIMS.Pages.Teacher.Courses
{
    public class IndexModel : PageModel
    {
        private readonly StudentCourseService _service;
        public INotyfService _notifyService { get; }
        public List<StudentCourse> StudentCourseList { get; set; }


        public IndexModel(StudentCourseService service, INotyfService notifyService)
        {
            _service = service;
            _notifyService = notifyService;
        }

        public void OnGet()
        {
            StudentCourseList = _service.GetStudentCourses().ToList();
        }
        public async Task<IActionResult> OnPostDeleteAsync(string studentNo)
        {
            if (string.IsNullOrEmpty(studentNo))
            {
                return NotFound();
            }

            // Logic to delete student based on studentNo
            _service.DeleteStudentCourse(studentNo);
            _notifyService.Success("Xóa thành công");

            // Redirect to index or wherever needed after deletion
            return RedirectToPage("./Index");
        }
    }
}
