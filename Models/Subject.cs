#nullable disable
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }

        //Navigation Property
        public int StudentId { get; set; }  
        public Student Student { get; set; }
        public ICollection<StudentSubjectPerformance> StudentSubjectPerformances { get; set; }
    }
}
