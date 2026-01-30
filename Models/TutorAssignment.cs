using System.ComponentModel.DataAnnotations;
#nullable disable

namespace SchoolManagementSystem.Models
{
    public class TutorAssignment
    {
        [Key]
        public int TutorAssignmentId { get; set; }
        public string Notes { get; set; }
        public DateTime SessionDateTime { get; set; }

        //Navigatuion Property
        public int TutorId { get; set; }
        public Tutor Tutor { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
