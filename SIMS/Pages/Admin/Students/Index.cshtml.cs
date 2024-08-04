using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIMS.Model;
using SIMS.Services;

namespace SIMS.Pages.Admin.Students
{
    public class IndexModel : PageModel
    {
        private readonly StudentService _service;
        public INotyfService _notifyService { get; }
        public List<Student> StudentList { get; set; }


        public IndexModel(StudentService service, INotyfService notifyService)
        {
            _service = service;
            _notifyService = notifyService;
        }

        public void OnGet()
        {
            StudentList = _service.GetStudents().ToList();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string studentNo)
        {
            if (string.IsNullOrEmpty(studentNo))
            {
                return NotFound();
            }

            // Logic to delete student based on studentNo
            _service.DeleteStudent(studentNo);
            _notifyService.Success("X�a th�nh c�ng");

            // Redirect to index or wherever needed after deletion
            return RedirectToPage("./Index");
        }
    }
}
