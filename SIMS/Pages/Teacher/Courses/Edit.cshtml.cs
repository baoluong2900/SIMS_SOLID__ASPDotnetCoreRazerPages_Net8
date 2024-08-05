using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIMS.Model;
using SIMS.Services;

namespace SIMS.Pages.Teacher.Courses
{
    public class EditModel : PageModel
    {
        private readonly StudentCourseService _service;

        public INotyfService _notifyService { get; }

        public List<StudentCourse> StudentCourseList { get; set; }

        [BindProperty]
        public StudentCourse OldStudentCourse { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            OldStudentCourse = _service.GetStudentCourse(id);
            return Page();
        }
        public EditModel(StudentCourseService service, INotyfService notifyService)
        {
            _service = service;
            _notifyService = notifyService;
        }

        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid || OldStudentCourse == null)
                {
                    return Page();
                }
                _service.UpdateStudentCourse(OldStudentCourse);
                // Logic to update the StudentCourse to the database goes here
                _notifyService.Success("Cập nhật thành công");
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
