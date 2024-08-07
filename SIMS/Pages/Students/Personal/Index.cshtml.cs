using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIMS.Model;
using SIMS.Services;

namespace SIMS.Pages.Students.Personal
{
    public class IndexModel : PageModel
    {
        private readonly StudentCourseService _service;

        public INotyfService _notifyService { get; }

        public List<StudentCourse> StudentCourseList { get; set; }

        private string idLogin;

        [BindProperty]
        public StudentCourse OldStudentCourse { get; set; }

        public async void OnGetAsync(string id)
        {
            if(string.IsNullOrEmpty(idLogin) && !string.IsNullOrEmpty(id))
            {
                idLogin = id;
                HttpContext.Session.SetString("idLogin", idLogin);
            }
            else
            {
                idLogin = HttpContext.Session.GetString("idLogin");
            }
            OldStudentCourse = _service.GetStudentCourse(idLogin);
        }
        public IndexModel(StudentCourseService service, INotyfService notifyService)
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
                return RedirectToPage("/Students/Personal/Index", new { id = idLogin });

            }
            catch (Exception ex)
            {
                _notifyService.Error("Đã có lỗi ngoại lệ xảy ra");
                return Page();
            }
        }
    }
}
