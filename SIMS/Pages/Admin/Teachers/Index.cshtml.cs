using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIMS.Model;
using SIMS.Services;

namespace SIMS.Pages.Admin.Teachers
{
    public class IndexModel : PageModel
    {
        private readonly TeacherService _service;
        public INotyfService _notifyService { get; }
        public List<Model.Teacher> TeacherList { get; set; }

        public IndexModel(TeacherService service, INotyfService notifyService)
        {
            _service = service;
            _notifyService = notifyService;
        }

        public void OnGet()
        {
            TeacherList = _service.GetTeachers().ToList();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string teacherNo)
        {
            if (string.IsNullOrEmpty(teacherNo))
            {
                return NotFound();
            }

            // Logic to delete teacher based on teacherNo
            _service.DeleteTeacher(teacherNo);
            _notifyService.Success("Xóa thành công");

            // Redirect to index or wherever needed after deletion
            return RedirectToPage("./Index");
        }
    }

}
