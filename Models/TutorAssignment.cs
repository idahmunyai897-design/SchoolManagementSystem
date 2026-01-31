#nullable disable
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class TutorAssignment
    {
        [Key]
        public int TutorAssignmentId { get; set; }

        [MaxLength(50)]
        public string Notes { get; set; }

        [DataType(DataType.Date)]
        public DateTime SessionDateTime { get; set; }

        // Foreign Keys
        public int TutorId { get; set; }
        public int SubjectId { get; set; }
        public int StudentId { get; set; }

        // Navigation
        public Tutor Tutor { get; set; }
        public Subject Subject { get; set; }
        public Student Student { get; set; }
    }
}
