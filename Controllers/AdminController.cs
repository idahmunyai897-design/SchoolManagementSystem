using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
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
            // Basic counts
            ViewBag.TotalStudents = _context.Students.Count();
            ViewBag.TotalTeachers = _context.Teachers.Count();
            ViewBag.TotalTutors = _context.Tutors.Count();
            ViewBag.TotalSubjects = _context.Subjects.Count();

            return View();
        }

        // ============================
        // STUDENTS MANAGEMENT
        // ============================
        public IActionResult Students()
        {
            var students = _context.Students
                .ToList();

            return View(students);
        }

        public IActionResult CreateStudent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Students));
            }
            return View(student);
        }

        // ============================
        // SUBJECTS MANAGEMENT
        // ============================
        public IActionResult Subjects()
        {
            var subjects = _context.Subjects
                .ToList();

            return View(subjects);
        }

        public IActionResult CreateSubject()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSubject(Subject subject)
        {
            if (ModelState.IsValid)
            {
                _context.Subjects.Add(subject);
                _context.SaveChanges();
                return RedirectToAction(nameof(Subjects));
            }
            return View(subject);
        }

        // ============================
        // TEACHERS MANAGEMENT
        // ============================
        public IActionResult Teachers()
        {
            var teachers = _context.Teachers
                .ToList();

            return View(teachers);
        }

        public IActionResult CreateTeacher()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTeacher(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Teachers.Add(teacher);
                _context.SaveChanges();
                return RedirectToAction(nameof(Teachers));
            }
            return View(teacher);
        }

        // ============================
        // TUTORS MANAGEMENT
        // ============================
        public IActionResult Tutors()
        {
            var tutors = _context.Tutors
                .ToList();

            return View(tutors);
        }

        public IActionResult CreateTutor()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTutor(Tutor tutor)
        {
            if (ModelState.IsValid)
            {
                _context.Tutors.Add(tutor);
                _context.SaveChanges();
                return RedirectToAction(nameof(Tutors));
            }
            return View(tutor);
        }

        // ============================
        // ASSIGN SUBJECTS TO STUDENTS
        // ============================
        public IActionResult AssignSubjects()
        {
            ViewBag.Students = _context.Students
                .ToList();

            ViewBag.Subjects = _context.Subjects
                .ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignSubjects(int studentId, int subjectId)
        {
            var exists = _context.StudentSubjectPerformances
                .Any(s => s.StudentId == studentId && s.SubjectId == subjectId);

            if (!exists)
            {
                var newAssignment = new StudentSubjectPerformance
                {
                    StudentId = studentId,
                    SubjectId = subjectId,
                    Score = 0,
                    Comments = ""
                };
                _context.StudentSubjectPerformances.Add(newAssignment);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Students));
        }

        // ============================
        // ASSIGN SUBJECTS TO TEACHERS / TUTORS
        // ============================
        public IActionResult AssignTutor()
        {
            ViewBag.Tutors = _context.Tutors.ToList();
            ViewBag.Students = _context.Students.ToList();
            ViewBag.Subjects = _context.Subjects.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignTutor(int tutorId, int studentId, int subjectId)
        {
            var exists = _context.TutorAssignments
                .Any(t => t.TutorId == tutorId && t.StudentId == studentId && t.SubjectId == subjectId);

            if (!exists)
            {
                var newAssignment = new TutorAssignment
                {
                    TutorId = tutorId,
                    StudentId = studentId,
                    SubjectId = subjectId
                };
                _context.TutorAssignments.Add(newAssignment);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Tutors));
        }

        // ============================
        // REPORTS
        // ============================
        public IActionResult Report()
        {
            ViewBag.TotalStudents = _context.Students
                .Count();
            ViewBag.TotalTeachers = _context.Teachers
                .Count();
            ViewBag.TotalTutors = _context.Tutors
                .Count();
            ViewBag.TotalSubjects = _context.Subjects
                .Count();

            // E.g weak students (score < 50)
            ViewBag.WeakStudents = _context.StudentSubjectPerformances
                .Where(s => s.Score < 50)
                .Select(s => s.StudentId)
                .Distinct()
                .Count();

            return View();
        }

        // ============================
        // ASSIGN MAJOR + TRACK
        // ============================

        public IActionResult AssignMajor(int studentId)
        {
            ViewBag.Student = _context.Students.Find(studentId);
            ViewBag.Majors = _context.Majors.ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignMajor(int studentId, int majorId, string mathTrack, string scienceTrack)
        {
            var student = _context.Students.Find(studentId);

            if (student != null)
            {
                student.MajorId = majorId;
                student.MathTrack = mathTrack;
                student.ScienceTrack = scienceTrack;

                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Students));
        }

        public IActionResult AutoAssignSubjects(int studentId)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentId == studentId);

            if (student == null)
                return RedirectToAction("Students");

            int grade = int.Parse(student.GradeLevel);

            List<Subject> subjects;

            if (grade < 10)
            {
                subjects = _context.Subjects
                    .Where(s => s.GradeFrom <= grade && s.GradeTo >= grade)
                    .ToList();
            }
            else
            {
                subjects = _context.Subjects
                    .Where(s => s.GradeFrom <= grade && s.GradeTo >= grade)
                    .ToList();
            }

            foreach (var subject in subjects)
            {
                bool exists = _context.StudentSubjectPerformances
                    .Any(x => x.StudentId == studentId && x.SubjectId == subject.SubjectId);

                if (!exists)
                {
                    _context.StudentSubjectPerformances.Add(new StudentSubjectPerformance
                    {
                        StudentId = studentId,
                        SubjectId = subject.SubjectId,
                        Score = 0,
                        StudentPerformanceLevel = "Pending",
                        TeacherPerformanceLevel = "Pending",
                        DateRecorded = DateTime.Now
                    });
                }
            }

            _context.SaveChanges();

            return RedirectToAction("Students");
        }

    }
}
