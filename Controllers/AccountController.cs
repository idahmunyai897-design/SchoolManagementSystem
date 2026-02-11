using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.ViewModel;
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

        // GET SignUp
        public IActionResult SignUp()
        {
            // Roles for the dropdown (Admin removed)
            ViewBag.Roles = new[] { "Student", "Teacher", "Tutor" };
            return View();
        }

        // POST SignUp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(SignUpViewModel model)
        {
            // Pass roles again if we need to redisplay the form
            ViewBag.Roles = new[] { "Student", "Teacher", "Tutor" };

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Make sure a role is selected
            if (string.IsNullOrWhiteSpace(model.Role))
            {
                ModelState.AddModelError("Role", "Please select a role.");
                return View(model);
            }

            // Handle registration based on role
            switch (model.Role)
            {
                case "Student":
                    if (_context.Students.Any(s => s.Email == model.Email))
                    {
                        ModelState.AddModelError("", "Email is already registered.");
                        return View(model);
                    }

                    _context.Students.Add(new Student
                    {
                        FullNames = model.Name,
                        Email = model.Email,
                        Password = model.Password,
                        Role = "Student"
                    });
                    break;

                case "Teacher":
                    if (_context.Teachers.Any(t => t.Email == model.Email))
                    {
                        ModelState.AddModelError("", "Email is already registered.");
                        return View(model);
                    }

                    _context.Teachers.Add(new Teacher
                    {
                        FullNames = model.Name,
                        Email = model.Email,
                        Password = model.Password,
                        Role = "Teacher"
                    });
                    break;

                case "Tutor":
                    if (_context.Tutors.Any(tu => tu.Email == model.Email))
                    {
                        ModelState.AddModelError("", "Email is already registered.");
                        return View(model);
                    }

                    _context.Tutors.Add(new Tutor
                    {
                        FullNames = model.Name,
                        Email = model.Email,
                        Password = model.Password,
                        Role = "Tutor"
                    });
                    break;

                default:
                    ModelState.AddModelError("", "Invalid role selected.");
                    return View(model);
            }

            _context.SaveChanges();

            // Show success message on Login page
            TempData["SuccessMessage"] = "Registration successful! Please log in.";
            return RedirectToAction("Login");
        }
    }
}
