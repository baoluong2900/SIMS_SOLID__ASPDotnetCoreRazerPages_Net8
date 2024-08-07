using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIMS.Services;

namespace SIMS.Pages.Admin.Teachers
{
    public class CreateModel : PageModel
    {
        private readonly TeacherService _service;

        public INotyfService _notifyService { get; }

        public List<Model.Teacher> TeacherList { get; set; }

        [BindProperty]
        public Model.Teacher NewTeacher { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public CreateModel(TeacherService service, INotyfService notifyService)
        {
            _service = service;
            _notifyService = notifyService;
        }

        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid || NewTeacher == null)
                {
                    return Page();
                }
                bool checkTeacherNo = _service.GetTeachers().Any(x => x.TeacherNo == NewTeacher.TeacherNo);
                if (checkTeacherNo)
                {
                    _notifyService.Warning("Mã giảng viên đã tồn tại");
                    return Page();
                }
                _service.AddTeacher(NewTeacher);
                // Logic to save the teacher to the database goes here
                _notifyService.Success("Thêm thành công");
                return RedirectToPage("/Admin/Teachers/Index");
            }
            catch (Exception ex)
            {
                _notifyService.Error("Đã có lỗi ngoại lệ xảy ra");
                return Page();
            }
        }
    }

}
