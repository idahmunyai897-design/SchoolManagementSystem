#nullable disable
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }
        public string FullNames { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Qualification { get; set; }
        public string Specialization { get; set; }
        public DateTime DateJoined { get; set; }
        public string Address { get; set; }
        [Required]
        public string Grade { get; set; }

        //Navigation Property
        public ICollection<StudentSubjectPerformance> StudentSubjectPerformances { get; set; }

    }
}
