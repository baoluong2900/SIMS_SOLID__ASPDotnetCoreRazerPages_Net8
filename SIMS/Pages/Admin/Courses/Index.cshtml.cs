using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIMS.Model;
using SIMS.Services;

namespace SIMS.Pages.Admin.Courses
{
    public class IndexModel : PageModel
    {
        private readonly CourseService _service;
        public INotyfService _notifyService { get; }
        public List<Course> CourseList { get; set; }


        public IndexModel(CourseService service, INotyfService notifyService)
        {
            _service = service;
            _notifyService = notifyService;
        }

        public void OnGet()
        {
            CourseList = _service.GetCourses().ToList();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string studentNo)
        {
            if (string.IsNullOrEmpty(studentNo))
            {
                return NotFound();
            }

            // Logic to delete student based on studentNo
            _service.DeleteCourse(studentNo);
            _notifyService.Success("Xóa thành công");

            // Redirect to index or wherever needed after deletion
            return RedirectToPage("./Index");
        }
    }
}
