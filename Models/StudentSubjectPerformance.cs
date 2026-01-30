using System.ComponentModel.DataAnnotations;
#nullable disable

namespace SchoolManagementSystem.Models
{
    public class StudentSubjectPerformance
    {
        [Key]
        public int StudentSubjectPerformanceId { get; set; }
        public string StudentPerformanceLevel { get; set; }
        public string TeacherPerformanceLevel { get; set; }
        public DateTime DateRecorded { get; set; }

        //Navigation Property
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
