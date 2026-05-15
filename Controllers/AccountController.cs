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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
                return View(login);

            // ================= ADMIN LOGIN =================
            var admin = _context.Admins
                .FirstOrDefault(a => a.Email == login.Email && a.Password == login.Password);

            if (admin != null)
            {
                HttpContext.Session.SetString("UserEmail", admin.Email);
                HttpContext.Session.SetString("UserName", admin.FullNames);
                HttpContext.Session.SetString("UserRole", admin.Role);

                return RedirectToAction("Index", "Admin");
            }

            // ================= STUDENT =================
            var student = _context.Students
                .FirstOrDefault(s => s.Email == login.Email && s.Password == login.Password);

            if (student != null)
            {
                HttpContext.Session.SetString("UserEmail", student.Email);
                HttpContext.Session.SetString("UserName", student.FullNames);
                HttpContext.Session.SetString("UserRole", student.Role);

                return RedirectToAction("Index", "Student");
            }

            // ================= TEACHER =================
            var teacher = _context.Teachers
                .FirstOrDefault(t => t.Email == login.Email && t.Password == login.Password);

            if (teacher != null)
            {
                HttpContext.Session.SetString("UserEmail", teacher.Email);
                HttpContext.Session.SetString("UserName", teacher.FullNames);
                HttpContext.Session.SetString("UserRole", teacher.Role);

                return RedirectToAction("Index", "Teacher");
            }

            // ================= TUTOR =================
            var tutor = _context.Tutors
                .FirstOrDefault(tu => tu.Email == login.Email && tu.Password == login.Password);

            if (tutor != null)
            {
                HttpContext.Session.SetString("UserEmail", tutor.Email);
                HttpContext.Session.SetString("UserName", tutor.FullNames);
                HttpContext.Session.SetString("UserRole", tutor.Role);

                return RedirectToAction("Index", "Tutor");
            }

            ModelState.AddModelError("", "Invalid email or password");
            return View(login);
        }

        // GET: Account/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
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
            ViewBag.Roles = new[] { "Student", "Teacher", "Tutor" };

            if (!ModelState.IsValid)
                return View(model);

            if (string.IsNullOrWhiteSpace(model.Role))
            {
                ModelState.AddModelError("Role", "Please select a role.");
                return View(model);
            }

            // Store basic info temporarily
            TempData["Name"] = model.Name;
            TempData["Email"] = model.Email;
            TempData["Password"] = model.Password;
            TempData["Role"] = model.Role;

            switch (model.Role)
            {
                case "Student":
                    return RedirectToAction("StudentDetails");

                case "Teacher":
                    return RedirectToAction("TeacherDetails");

                case "Tutor":
                    return RedirectToAction("TutorDetails");

                default:
                    ModelState.AddModelError("", "Invalid role selected.");
                    return View(model);
            }
        }


        // GET StudentDetails
        public IActionResult StudentDetails()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StudentDetails(int Age, string Address, string GuardianName, string GuardianContact, string GradeLevel)
        {
            var student = new Student
            {
                FullNames = TempData["Name"].ToString(),
                Email = TempData["Email"].ToString(),
                Password = TempData["Password"].ToString(),

                Age = Age,
                Address = Address,
                GuardianName = GuardianName,
                GuardianContact = GuardianContact,
                GradeLevel = GradeLevel,

                DateEnrolled = DateTime.Now,
                DateOfBirth = DateTime.Now.AddYears(-Age),
                Role = "Student"
            };

            _context.Students.Add(student);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Registration successful! Please log in.";

            return RedirectToAction("Login");
        }


    }
}
