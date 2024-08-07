using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIMS.Model;
using SIMS.Services;

namespace SIMS.Pages.Admin.Courses
{
    public class CreateModel : PageModel
    {
        private readonly CourseService _service;

        public INotyfService _notifyService { get; }

        public List<Course> CourseList { get; set; }

        [BindProperty]
        public Course NewCourse { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }
        public CreateModel(CourseService service, INotyfService notifyService)
        {
            _service = service;
            _notifyService = notifyService;
        }

        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid || NewCourse == null)
                {
                    return Page();
                }
                bool checkCourseNo = _service.GetCourses().Any(x => x.CourseCode == NewCourse.CourseCode);
                if (checkCourseNo)
                {
                    _notifyService.Warning("Mã sinh viên đã tồn tại");
                    return Page();
                }
                _service.AddCourse(NewCourse);
                // Logic to save the Course to the database goes here
                _notifyService.Success("Thêm thành công");
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
