#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class StudentSubjectPerformance
    {
        [Key]
        public int StudentSubjectPerformanceId { get; set; }

        [Required, StringLength(50)]
        public string StudentPerformanceLevel { get; set; }  // Struggling / Good

        [Required, StringLength(50)]
        public string TeacherPerformanceLevel { get; set; }  // Teacher evaluation

        [DataType(DataType.Date)]
        public DateTime DateRecorded { get; set; }

        // Foreign Keys
        public int StudentId { get; set; }
        public int SubjectId { get; set; }

        // Tutor and Peer Helper
        public int? PeerHelperId { get; set; } // links to another Student
        public string Comments { get; set; }
        public double Score { get; set; }

        // Navigation
        public Student Student { get; set; }
        public Subject Subject { get; set; }
        public Student PeerHelper { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
