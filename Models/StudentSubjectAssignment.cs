using System.ComponentModel.DataAnnotations;
#nullable disable

namespace SchoolManagementSystem.Models
{
    public class StudentSubjectAssignment
    {
        [Key]
        public int StudentSubjectAssignmentId { get; set; }

        public int StudentId { get; set; }
        public int SubjectId { get; set; }

        // Track choice only matters for Math or Science
        public string TrackChoice { get; set; } // e.g., "Pure Math", "Technical Science"

        // Navigation
        public Student Student { get; set; }
        public Subject Subject { get; set; }
    }
}
