using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIMS.Model;
using SIMS.Services;

namespace SIMS.Pages.Students.Grades
{
    public class IndexModel : PageModel
    {
        private string idLogin;

        private readonly GradeService _service;
        public INotyfService _notifyService { get; }
        public List<Grade> GradeList { get; set; }


        public async void OnGetAsync()
        {
            idLogin = HttpContext.Session.GetString("idLogin");
            GradeList = _service.GetGradesByStudentId(idLogin).ToList();
        }

        public IndexModel(GradeService service, INotyfService notifyService)
        {
            _service = service;
            _notifyService = notifyService;
        }
    }
}
