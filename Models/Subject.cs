#nullable disable
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }

        [Required, StringLength(100)]
        public string SubjectName { get; set; }

        // Navigation
        public ICollection<StudentSubjectPerformance> StudentSubjectPerformances { get; set; }
        public ICollection<TutorAssignment> TutorAssignments { get; set; }
    }
}
