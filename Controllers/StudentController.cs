using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly SchoolDbContext _context;

        // Constructor: gives the controller access to the database
        public StudentController(SchoolDbContext context)
        {
            _context = context;
        }

        //View all students together with their Major subject
        public IActionResult Index()
        {
            var students = _context.Students
                         .Include(s => s.Major)
                         .ToList();

            return View(students);
        }

        //View 1 student per 
        public IActionResult Details(int id)
        {
            var student = _context.Students
                                  .Include(s => s.Major)
                                  .Include(s => s.StudentSubjectPerformances)
                                  .ThenInclude(ssp => ssp.Subject)
                                  .FirstOrDefault(s => s.StudentId == id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        //Adding students/ creatung student 
        public IActionResult Create()
        {
            ViewBag.Majors = _context.Majors //ViewBag lets the form show all the majors for grade 10 learners
                .ToList();

            return View();
        }

        //This is for submitting that form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid) //checks if all required fields and validations are okay
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Majors = _context.Majors.ToList();
            return View(student);
        }

        //GET: form its for when i want to edit a student infor
        public IActionResult Edit(int id)
        {
            var student = _context.Students
                .Find(id);

            if (student == null) return NotFound();

            ViewBag.Majors = _context.Majors
                .ToList();

            return View(student);
        }

        //Submit update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student student)
        {
            //the if its for getting the student by their primary key
            if (ModelState.IsValid)
            {
                _context.Students.Update(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Majors = _context.Majors.ToList();
            return View(student);
        }

        //Deleting the student
        public IActionResult Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                // Soft delete
                student.IsDeleted = true;
                _context.Students.Update(student);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
