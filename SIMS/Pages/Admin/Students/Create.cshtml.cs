﻿using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIMS.Model;
using SIMS.Services;

namespace SIMS.Pages.Admin.Students
{
    public class CreateModel : PageModel
    {
        private readonly StudentService _service;

        public INotyfService _notifyService { get; }

        public List<Student> StudentList { get; set; }

        [BindProperty]
        public Student NewStudent { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }
        public CreateModel(StudentService service, INotyfService notifyService)
        {
            _service = service;
            _notifyService = notifyService;
        }

        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid || NewStudent == null)
                {
                    return Page();
                }
                bool checkStudentNo = _service.GetStudents().Any(x => x.StudentNo == NewStudent.StudentNo);
                if (checkStudentNo)
                {
                    _notifyService.Warning("Mã sinh viên đã tồn tại");
                    return Page();
                }
                _service.AddStudent(NewStudent);
                // Logic to save the student to the database goes here
                _notifyService.Success("Thêm thành công");
                return RedirectToPage("/Admin/Students/Index");
            }
            catch (Exception ex) {
                _notifyService.Error("Đã có lỗi ngoại lệ xảy ra");
                return Page();
            }
        }
    }
}
