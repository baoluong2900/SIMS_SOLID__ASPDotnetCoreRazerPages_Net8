using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIMS.Model;
using SIMS.Services;

namespace SIMS.Pages.Admin.Grades
{
    public class IndexModel : PageModel
    {
        private readonly GradeService _service;
        public INotyfService _notifyService { get; }
        public List<Grade> GradeList { get; set; }

        public IndexModel(GradeService service, INotyfService notifyService)
        {
            _service = service;
            _notifyService = notifyService;
        }

        public void OnGet()
        {
            GradeList = _service.GetGrades().ToList();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string gradeId)
        {
            if (string.IsNullOrEmpty(gradeId))
            {
                return NotFound();
            }

            // Logic to delete grade based on gradeId
            _service.DeleteGrade(gradeId);
            _notifyService.Success("Xóa thành công");

            // Redirect to index or wherever needed after deletion
            return RedirectToPage("./Index");
        }
    }
}
