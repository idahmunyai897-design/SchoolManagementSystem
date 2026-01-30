#nullable disable
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class TeacherPerformance
    {
        [Key]
        public int TeacherPerformanceId { get; set; }

        public int TeacherId { get; set; }
        public int SubjectId { get; set; }

        public string PerformanceLevel { get; set; }

        // Navigation
        public Teacher Teacher { get; set; }
        public Subject Subject { get; set; }
    }
}
