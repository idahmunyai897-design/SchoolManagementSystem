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
        public bool IsDeleted { get; set; } = false; // false = active, true = deleted
        public int GradeFrom { get; set; }
        public int GradeTo { get; set; }
        public bool RequiresTrack { get; set; } // true for Math or Science

        // Navigation
        public ICollection<StudentSubjectPerformance> StudentSubjectPerformances { get; set; }
        public ICollection<TutorAssignment> TutorAssignments { get; set; }
    }
}
