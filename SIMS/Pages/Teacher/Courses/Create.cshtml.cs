using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIMS.Model;
using SIMS.Services;

namespace SIMS.Pages.Teacher.Courses
{
    public class CreateModel : PageModel
    {
        private readonly StudentCourseService _service;

        public INotyfService _notifyService { get; }

        public List<StudentCourse> StudentCourseList { get; set; }

        [BindProperty]
        public StudentCourse NewStudentCourse { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }
        public CreateModel(StudentCourseService service, INotyfService notifyService)
        {
            _service = service;
            _notifyService = notifyService;
        }

        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid || NewStudentCourse == null)
                {
                    return Page();
                }
                bool checkStudentCourseNo = _service.GetStudentCourses().Any(x => x.StudentCourseNo == NewStudentCourse.StudentCourseNo);
                if (checkStudentCourseNo)
                {
                    _notifyService.Warning("Mã sinh viên đã tồn tại");
                    return Page();
                }
                _service.AddStudentCourse(NewStudentCourse);
                // Logic to save the StudentCourse to the database goes here
                _notifyService.Success("Thêm thành công");
                return RedirectToPage("/Teacher/Courses/Index");
            }
            catch (Exception ex)
            {
                _notifyService.Error("Đã có lỗi ngoại lệ xảy ra");
                return Page();
            }
        }
    }
}
