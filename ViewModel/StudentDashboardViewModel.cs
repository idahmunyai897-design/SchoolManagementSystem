using SchoolManagementSystem.Models;
#nullable disable

namespace SchoolManagementSystem.ViewModel
{
    public class StudentDashboardViewModel
    {
        public Student Student { get; set; }
        public List<StudentSubjectPerformance> Performances { get; set; }
        public List<TutorAssignment> TutorAssignments { get; set; }
    }
}
