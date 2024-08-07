using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIMS.Model;
using SIMS.Services;

namespace SIMS.Pages.Admin.Courses
{
    public class EditModel : PageModel
    {
        private readonly CourseService _service;

        public INotyfService _notifyService { get; }

        public List<Course> CourseList { get; set; }

        [BindProperty]
        public Course OldCourse { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            OldCourse = _service.GetCourse(id);
            return Page();
        }
        public EditModel(CourseService service, INotyfService notifyService)
        {
            _service = service;
            _notifyService = notifyService;
        }

        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid || OldCourse == null)
                {
                    return Page();
                }
                _service.UpdateCourse(OldCourse);
                // Logic to update the course to the database goes here
                _notifyService.Success("Cập nhật thành công");
                return RedirectToPage("/Admin/Students/Index");

            }
            catch (Exception ex)
            {
                _notifyService.Error("Đã có lỗi ngoại lệ xảy ra");
                return Page();
            }
        }
    }
}
