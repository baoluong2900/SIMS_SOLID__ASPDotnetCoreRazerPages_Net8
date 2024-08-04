using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIMS.Model;
using SIMS.Services;

namespace SIMS.Pages.Admin.Students
{
    public class EditModel : PageModel
    {
        private readonly StudentService _service;

        public INotyfService _notifyService { get; }

        public List<Student> StudentList { get; set; }

        [BindProperty]
        public Student OldStudent { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            OldStudent = _service.GetStudent(id);
            return Page();
        }
        public EditModel(StudentService service, INotyfService notifyService)
        {
            _service = service;
            _notifyService = notifyService;
        }

        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid || OldStudent == null)
                {
                    return Page();
                }
                _service.UpdateStudent(OldStudent);
                // Logic to update the student to the database goes here
                _notifyService.Success("Cập nhật thành công");
                return RedirectToPage("/Admin/Students/Index");

            }
            catch (Exception ex)
            {
                _notifyService.Error("Đã có lỗi ngoại lệ xảy ra");
                return Page();
            }
        }
    }
}
