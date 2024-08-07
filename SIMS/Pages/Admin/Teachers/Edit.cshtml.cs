using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIMS.Services;

namespace SIMS.Pages.Admin.Teachers
{
    public class EditModel : PageModel
    {
        private readonly TeacherService _service;

        public INotyfService _notifyService { get; }

        public List<Model.Teacher> TeacherList { get; set; }

        [BindProperty]
        public Model.Teacher OldTeacher { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            OldTeacher = _service.GetTeacher(id);
            return Page();
        }
        public EditModel(TeacherService service, INotyfService notifyService)
        {
            _service = service;
            _notifyService = notifyService;
        }

        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid || OldTeacher == null)
                {
                    return Page();
                }
                _service.UpdateTeacher(OldTeacher);
                // Logic to update the teacher in the database goes here
                _notifyService.Success("Cập nhật thành công");
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
