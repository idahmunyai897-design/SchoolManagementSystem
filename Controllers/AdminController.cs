using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Data;
using System.Linq;

namespace SchoolManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly SchoolDbContext _context;

        public AdminController(SchoolDbContext context)
        {
            _context = context;
        }

        // ============================
        // DASHBOARD
        // ============================
        public IActionResult Index()
        {
            ViewBag.TotalStudents = _context.Students.Count();
            ViewBag.TotalTeachers = _context.Teachers.Count();
            ViewBag.TotalTutors = _context.Tutors.Count();
            ViewBag.TotalSubjects = _context.Subjects.Count();

            return View();
        }

        // ============================
        // REPORTS
        // ============================
        public IActionResult Report()
        {
            ViewBag.TotalStudents = _context.Students.Count();
            ViewBag.TotalTeachers = _context.Teachers.Count();
            ViewBag.TotalTutors = _context.Tutors.Count();
            ViewBag.TotalSubjects = _context.Subjects.Count();

            ViewBag.WeakStudents = _context.StudentSubjectPerformances
                .Where(s => s.Score < 50)
                .Select(s => s.StudentId)
                .Distinct()
                .Count();

            return View();
        }
    }
}