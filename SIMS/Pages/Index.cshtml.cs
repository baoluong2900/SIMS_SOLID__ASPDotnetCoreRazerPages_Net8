using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIMS.Abstractions;
using SIMS.Model;
using SIMS.Services;

namespace SIMS.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public INotyfService _notifyService { get; }

        private readonly AuthService _service;

        [BindProperty]
        public UserLogin UserViewModel { get; set; }
        public IndexModel(ILogger<IndexModel> logger, INotyfService notifyService, AuthService service)
        {
            _logger = logger;
            _notifyService = notifyService;
            _service = service;

        }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostIndexAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Warning("Vui lòng thông tin tài khoản");
                    return Page();
                }

                var role = _service.Login(UserViewModel.UserName, UserViewModel.Password);

                if (!string.IsNullOrEmpty(role))
                {
                    switch (role.ToUpper())
                    {
                        case "STUDENT":
                            return Redirect("/Student");
                        case "ADMIN":
                            return Redirect("/Admin/Dashboard");
                        case "TEACHER":
                            return Redirect("/Teacher");
                        default:
                            _notifyService.Warning("Vai trò không hợp lệ");
                            return Page();
                    }
                }
                else
                {
                    _notifyService.Warning("Thông tin đăng nhập không đúng");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                _notifyService.Error("Đã có lỗi ngoại lệ xảy ra");
                // Log exception details if necessary
                return Page();
            }
        }
    }
}
