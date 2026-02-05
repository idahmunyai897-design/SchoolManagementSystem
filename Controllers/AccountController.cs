using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using System.Linq;

namespace SchoolManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly SchoolDbContext _context;

        public AccountController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET Login
        public IActionResult Login()
        {
            return View();
        }

        // POST Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var student = _context.Students
                    .FirstOrDefault(s => s.Email == login.Email && s.Password == login.Password);

                if (student != null)
                {
                    if (student.Role == "Admin")
                        return RedirectToAction("Index", "Admin");

                    return RedirectToAction("Index", "Student");
                }

                var teacher = _context.Teachers
                    .FirstOrDefault(t => t.Email == login.Email && t.Password == login.Password);

                if (teacher != null)
                    return RedirectToAction("Index", "Teacher");

                var tutor = _context.Tutors
                    .FirstOrDefault(tu => tu.Email == login.Email && tu.Password == login.Password);

                if (tutor != null)
                    return RedirectToAction("Index", "Tutor");

                ModelState.AddModelError("", "Invalid email or password");
            }

            return View(login);
        }
    }
}
