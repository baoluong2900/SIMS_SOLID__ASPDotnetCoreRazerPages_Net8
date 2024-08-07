using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIMS.Model;
using SIMS.Services;

namespace SIMS.Pages.Teacher.Grades
{
    public class CreateModel : PageModel
    {
        private readonly GradeService _service;

        public INotyfService _notifyService { get; }

        [BindProperty]
        public Grade NewGrade { get; set; }

        public CreateModel(GradeService service, INotyfService notifyService)
        {
            _service = service;
            _notifyService = notifyService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid || NewGrade == null)
                {
                    return Page();
                }
                bool checkGradeId = _service.GetGrades().Any(x => x.GradeId == NewGrade.GradeId);
                if (checkGradeId)
                {
                    _notifyService.Warning("Mã điểm đã tồn tại");
                    return Page();
                }
                _service.AddGrade(NewGrade);
                _notifyService.Success("Thêm thành công");
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
