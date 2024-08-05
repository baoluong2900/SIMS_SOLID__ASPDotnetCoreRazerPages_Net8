using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIMS.Model;
using SIMS.Services;

namespace SIMS.Pages.Teacher.Grades
{
    public class EditModel : PageModel
    {
        private readonly GradeService _service;

        public INotyfService _notifyService { get; }

        [BindProperty]
        public Grade OldGrade { get; set; }

        public EditModel(GradeService service, INotyfService notifyService)
        {
            _service = service;
            _notifyService = notifyService;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            OldGrade = _service.GetGrade(id);
            if (OldGrade == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid || OldGrade == null)
                {
                    return Page();
                }
                _service.UpdateGrade(OldGrade);
                _notifyService.Success("Cập nhật thành công");
                return RedirectToPage("/Teacher/Grades/Index");
            }
            catch (Exception ex)
            {
                _notifyService.Error("Đã có lỗi ngoại lệ xảy ra");
                return Page();
            }
        }
    }
}
