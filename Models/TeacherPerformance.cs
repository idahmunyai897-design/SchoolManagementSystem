#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class TeacherPerformance
    {
        [Key]
        public int TeacherPerformanceId { get; set; }
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }

        [Required, StringLength(50)]
        public string PerformanceLevel { get; set; }

        // Navigation
        public Teacher Teacher { get; set; }
        public Subject Subject { get; set; }
    }
}
