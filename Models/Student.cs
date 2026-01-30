#nullable disable
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string FullNames { get; set; }
        public string Email { get; set; }
        public string Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public DateTime DateEnrolled { get; set; }
        public string GuardianName { get; set; }
        public string GuardianContact { get; set; }
        public string GradeLevel { get; set; }

        //Navigation Property
        public ICollection<Subject> Subjects { get; set; }
        public ICollection<StudentSubjectPerformance> StudentSubjectPerformances { get; set; }
        public ICollection<TutorAssignment> TutorAssignments { get; set; }


    }
}
